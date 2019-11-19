using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemFieldController : MonoBehaviour
{
    public TileBase Tile_UnPlacable;
    public Tilemap tilemap_Item;

    public GameObject Item_Onmap_Prefab;
    Dictionary<Vector2Int, GameObject> Items_Onmap = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlaceAt(MapCoordinateInt position, ItemStack itemstack)
    {
        Vector2Int v2 = position.ToVector2Int();
        Vector3Int v3 = new Vector3Int(v2.x, 0, v2.y);
        if (tilemap_Item.GetTile(v3) == Tile_UnPlacable || tilemap_Item.HasTile(v3))
            return false;
        else
        {
            tilemap_Item.SetTile(ItemIndex.GetSprite());
            return true;
        }
    }

}
