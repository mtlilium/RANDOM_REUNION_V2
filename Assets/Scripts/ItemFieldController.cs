using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemFieldController : MonoBehaviour
{
    public TileBase Tile_UnPlacable;
    public Tilemap tilemap_Item;

    public GameObject Item_Onmap_Prefab;
    Dictionary<Vector3Int, Item> Items_Onmap = new Dictionary<Vector3Int, Item>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlaceAt(MapCoordinateInt position, Item item)
    {
        Vector2Int v2 = position.ToVector2Int();
        Vector3Int v3 = new Vector3Int(v2.x, 0, v2.y);
        if (tilemap_Item.GetTile(v3) == Tile_UnPlacable || tilemap_Item.HasTile(v3) || Items_Onmap.ContainsKey(v3))
            return false;
        else
        {
            tilemap_Item.SetTile(v3, ItemIndex.GetChip(item.ItemName));
            Items_Onmap.Add(v3, item);
            return true;
        }
    }

    public Item PickFrom(MapCoordinateInt position)
    {
        Vector2Int v2 = position.ToVector2Int();
        Vector3Int v3 = new Vector3Int(v2.x, 0, v2.y);
        if (!Items_Onmap.ContainsKey(v3))
            return null;
        else
        {
            var removedItem = Items_Onmap[v3];
            tilemap_Item.SetTile(v3, null);
            Items_Onmap.Remove(v3);
            return removedItem;
        }
    }

}
