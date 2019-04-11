using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    List<GameObject> go = new List<GameObject>();


    public GameObject[,] ReturnWallHere(Vector3 wallBottomLeft, GameObject Voxel, int wallHeight,int wallWidth)
    {
        int voxelAddressX =0;
        int voxelAddressY =0;
        GameObject[,] wall = new GameObject[wallWidth,wallHeight];
        float VoxelSize = Voxel.GetComponent<Renderer>().bounds.size.x;
        float WallHeightLength = wallHeight * VoxelSize;
        float WallWidthLength = wallWidth * VoxelSize;

        for (float y = 0; y < WallHeightLength; y += VoxelSize)
        {
            voxelAddressY++;
            for (float x = 0; x < WallWidthLength; x += VoxelSize)
            {

                Vector3 pos = new Vector3(wallBottomLeft.x + x, wallBottomLeft.y + y, wallBottomLeft.z);
                GameObject tempVoxel = Instantiate(Voxel, pos, Quaternion.identity);
                wall[voxelAddressX, voxelAddressY] = tempVoxel;
                tempVoxel.name = (voxelAddressX.ToString() + voxelAddressY.ToString());
                voxelAddressX++;
            }
        }
        return wall;

    }

    public void CreateHozWall(Vector3 wallBottomLeft, GameObject Voxel, int wallWidth , int wallHeight)
    {
        int voxelAddressX = 0;
        int voxelAddressY = 0;
        int count =0;
        float VoxelSize = Voxel.GetComponent<Renderer>().bounds.size.x;
        float WallHeightLength = wallHeight * VoxelSize;
        float WallWidthLength = wallWidth * VoxelSize;
        for (float y = 0; y < WallHeightLength; y += VoxelSize)
        {
            voxelAddressY++;
            for (float x = 0; x < WallWidthLength; x += VoxelSize)
            {

                Vector3 pos = new Vector3(wallBottomLeft.x + x, wallBottomLeft.y + y, wallBottomLeft.z);
                GameObject tempVoxel = Instantiate(Voxel, pos, Quaternion.identity);
                count++;
                go.Add( tempVoxel);

                tempVoxel.name = (voxelAddressX.ToString() + " , " + voxelAddressY.ToString());
                voxelAddressX++;
            }
        }
        


    }

    public void CreateWallFlipped(Vector3 wallBottomLeft, GameObject Voxel, int wallWidth, int wallHeight)
    {
        int voxelAddressX = 0;
        int voxelAddressY = 0;
        float VoxelSize = Voxel.GetComponent<Renderer>().bounds.size.x;
        float WallHeightLength = wallHeight * VoxelSize;
        float WallWidthLength = wallWidth * VoxelSize;
        
        for (float y = 0; y < WallHeightLength; y += VoxelSize)
        {
            voxelAddressY++;
            for (float x = 0; x < WallWidthLength; x += VoxelSize)
            {

                Vector3 pos = new Vector3(wallBottomLeft.x, wallBottomLeft.y + y, wallBottomLeft.z +x);
                GameObject tempVoxel = Instantiate(Voxel, pos, Quaternion.identity);
                tempVoxel.name = (voxelAddressX.ToString() + " , " + voxelAddressY.ToString());
                go.Add(tempVoxel);
                voxelAddressX++;
            }
        }

    }
    public void CreatePlane(Vector3 wallBottomLeft, GameObject Voxel, int wallWidth, int wallHeight)
    {
        int voxelAddressX = 0;
        int voxelAddressY = 0;
        float VoxelSize = Voxel.GetComponent<Renderer>().bounds.size.x;
        float WallHeightLength = wallHeight * VoxelSize;
        float WallWidthLength = wallWidth * VoxelSize;

        for (float y = 0; y < WallHeightLength; y += VoxelSize)
        {
            voxelAddressY++;
            for (float x = 0; x < WallWidthLength; x += VoxelSize)
            {

                Vector3 pos = new Vector3(wallBottomLeft.x + y, wallBottomLeft.y, wallBottomLeft.z + x);
                GameObject tempVoxel = Instantiate(Voxel, pos, Quaternion.identity);
                tempVoxel.name = (voxelAddressX.ToString() + " , " + voxelAddressY.ToString());
                go.Add(tempVoxel);
                voxelAddressX++;
            }
        }

    }

    public void CombineMeshes()
    {
        StaticBatchingUtility.Combine(go.ToArray(), gameObject);

    }


}
