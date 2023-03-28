using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class ItemDatabase : EditorWindow
{
    private VisualElement _itemsTab;
    private static VisualTreeAsset _itemRowTemplate;
    private ListView _itemListView;
    private float _itemHeight = 40;

    private ScrollView _detailItemSection;
    private ScrollView _detailMeleeSection;
    private ScrollView _detailGunSection;

    private VisualElement _largeDisplayIcon;
    private SO_Item _activeItem;

    private Sprite _defaultItemIcon;
    private static List<SO_Item> _itemDatabase = new List<SO_Item>();

    [MenuItem("Items/Item Database")]
    public static void Init()
    {
        ItemDatabase wnd = GetWindow<ItemDatabase>();
        wnd.titleContent = new GUIContent("Item Database");

        Vector2 size = new Vector2(900, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }

    public void CreateGUI()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI/ItemDatabase.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/UI/ItemDatabase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        //Import the ListView Item Template
        _itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI/ItemRowTemplate.uxml");
        _defaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/UnknownIcon.png", typeof(Sprite));

        LoadAllItems();

        _itemsTab = rootVisualElement.Q<VisualElement>("ItemsTab");
        GenerateListView();

        _detailItemSection = rootVisualElement.Q<ScrollView>("ScrollViewItem_Details");
        _detailMeleeSection = rootVisualElement.Q<ScrollView>("ScrollViewMelee_Details");
        _detailGunSection = rootVisualElement.Q<ScrollView>("ScrollViewGun_Details");

        _detailItemSection.style.display = DisplayStyle.None;
        _detailMeleeSection.style.display = DisplayStyle.None;
        _detailGunSection.style.display = DisplayStyle.None;

        _largeDisplayIcon = _detailItemSection.Q<VisualElement>("Icon");

        rootVisualElement.Q<DropdownField>("Drp_ItemToCreate").RegisterValueChangedCallback(evt => AddItem_DrpOnClick(evt.newValue));
        Debug.Log(_detailItemSection.name);
        //Register Value Changed Callbacks for new items added to the ListView

        if (_activeItem is SO_Melee melee)
        {
            _detailMeleeSection.Q<TextField>("MeleeName").RegisterValueChangedCallback(evt =>
            {
                Debug.Log(evt.newValue);
                melee.itemName = evt.newValue;
                _itemListView.Rebuild();
            });
        }
        else if (_activeItem is SO_Gun gun)
        {
            _detailGunSection.Q<TextField>("GunName").RegisterValueChangedCallback(evt =>
            {
                Debug.Log(evt.newValue);
                gun.itemName = evt.newValue;
                _itemListView.Rebuild();
            });
        }
        else if (_activeItem is SO_Item item)
        {
            _detailItemSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
            {
                Debug.Log(evt.newValue);
                item.itemName = evt.newValue;
                _itemListView.Rebuild();
            });
        }

        _detailItemSection.Q<ObjectField>("IconPicker").RegisterValueChangedCallback(evt =>
        {
            Sprite newSprite = evt.newValue as Sprite;
            _activeItem.itemSprite = newSprite == null ? _defaultItemIcon : newSprite;
            _largeDisplayIcon.style.backgroundImage = newSprite == null ? _defaultItemIcon.texture : newSprite.texture;

            _itemListView.Rebuild();

        });


        rootVisualElement.Q<Button>("Btn_DeleteItem").clicked += DeleteItem_OnClick;
    }

    private void DeleteItem_OnClick()
    {
        //Get the path of the fie and delete it through AssetDatabase
        string path = AssetDatabase.GetAssetPath(_activeItem);
        AssetDatabase.DeleteAsset(path);
        //Purge the reference from the list and refresh the ListView
        _itemDatabase.Remove(_activeItem);
        _itemListView.Rebuild();
        //Nothing is selected, so hide the details section
        _detailItemSection.style.display = DisplayStyle.None;
        _detailMeleeSection.style.display = DisplayStyle.None;
        _detailGunSection.style.display = DisplayStyle.None;
    }

    private void LoadAllItems()
    {
        _itemDatabase.Clear();
        string[] allPaths = Directory.GetFiles("Assets/Data", "*.asset", SearchOption.AllDirectories);
        foreach (string path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            _itemDatabase.Add((SO_Item)AssetDatabase.LoadAssetAtPath(cleanedPath, typeof(SO_Item)));
        }
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => _itemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage = _itemDatabase[i] == null ? _defaultItemIcon.texture : _itemDatabase[i].itemSprite.texture;
            e.Q<Label>("Name").text = _itemDatabase[i].itemName;
        };
        _itemListView = new ListView(_itemDatabase, 35, makeItem, bindItem);
        _itemListView.selectionType = SelectionType.Single;
        _itemListView.style.height = _itemDatabase.Count * _itemHeight;
        _itemsTab.Add(_itemListView);
        _itemListView.onSelectionChange += ListView_onSelectionChange;
    }

    private void ListView_onSelectionChange(IEnumerable<object> selectedItems)
    {
        _activeItem = (SO_Item)selectedItems.First();
        SerializedObject so = new SerializedObject(_activeItem);
        _detailItemSection.Bind(so);
        if (_activeItem.itemSprite != null)
        {
            _largeDisplayIcon.style.backgroundImage = _activeItem.itemSprite.texture;
        }
        _detailItemSection.style.display = DisplayStyle.Flex;

        //if (_activeItem is SO_Melee melee)
        //{
        //    SerializedObject so = new SerializedObject(melee);
        //    _detailItemSection.Bind(so);
        //    if (_activeItem.itemSprite != null)
        //    {
        //        _largeDisplayIcon.style.backgroundImage = _activeItem.itemSprite.texture;
        //    }
        //    _detailMeleeSection.style.display = DisplayStyle.Flex;
        //}
        //else if (_activeItem is SO_Gun gun)
        //{
        //    SerializedObject so = new SerializedObject(gun);
        //    _detailItemSection.Bind(so);
        //    if (_activeItem.itemSprite != null)
        //    {
        //        _largeDisplayIcon.style.backgroundImage = _activeItem.itemSprite.texture;
        //    }
        //    _detailGunSection.style.display = DisplayStyle.Flex;
        //}
        //else if (_activeItem is SO_Item item)
        //{
        //    SerializedObject so = new SerializedObject(item);
        //    _detailItemSection.Bind(so);
        //    if (_activeItem.itemSprite != null)
        //    {
        //        _largeDisplayIcon.style.backgroundImage = _activeItem.itemSprite.texture;
        //    }
        //    _detailItemSection.style.display = DisplayStyle.Flex;
        //}
    }

    private void AddItem_DrpOnClick(string index)
    {
        //Debug.Log($"Index: {index}");

        if (index.Contains("Item",StringComparison.CurrentCultureIgnoreCase))
        {
            //Create an instance of the scriptable object and set the default parameters
            SO_Item newItem = CreateInstance<SO_Item>();
            newItem.itemName = $"New Item";
            newItem.itemSprite = _defaultItemIcon;
            //Create the asset, using the unique ID for the name
            AssetDatabase.CreateAsset(newItem, $"Assets/Data/Item{newItem.itemId}.asset");
            //Add it to the item list
            _itemDatabase.Add(newItem);
            //Refresh the ListView so everything is redrawn again
            _itemListView.Rebuild();
            _itemListView.style.height = _itemDatabase.Count * _itemHeight;
        }
    }
}
