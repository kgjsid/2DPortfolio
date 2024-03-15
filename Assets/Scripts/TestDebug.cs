using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestDebug : MonoBehaviour
{
    [SerializeField] Tilemap tile;
    [SerializeField] AnimatedTile animatedTile;
    [SerializeField] TileBase tileBase;
    [SerializeField] TileAnimationData tileData;
    [SerializeField] GridManager gridManager;
    [SerializeField] PlayerMove player;
    [SerializeField] Animator animator;
    [SerializeField] SkillData skillData;

    private void Update()
    {
        // ��� �ִ� Ÿ�� 
        /*
        Vector3Int pos = tile.WorldToCell(player.gameObject.transform.position);
        if (tile.HasTile(pos))
        {
            animatedTile = (AnimatedTile)tile.GetTile(pos);
            
            animatedTile.GetTileAnimationData(pos, tile, ref tileData);

            if(tileData.flags == TileAnimationFlags.PauseAnimation)
            {
                tileData.animationStartTime = 1f;
            }
        }
        */
    }
    [ContextMenu("Use")]
    public void OnUse()
    {
        animator.Play("Ember");
    }

    [ContextMenu("Debug")]
    public void DebugTest()
    {
        Debug.Log($"{skillData.name}");
        
    }
}
