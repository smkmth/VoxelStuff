using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{
    public GameObject voxel;
    


    private CreateWall wallcreator;

    private void Start()
    {
        CreateARoom(transform.position, 100, 50, 100);
        //Vector3 pos = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z + 100);
       // CreateARoom(pos, 50, 50, 50);
   


    }

    public void CreateARoom( Vector3 pos, int RoomWidth, int WallHeight, int RoomLength )
    {
        float VoxelSize = voxel.GetComponent<Renderer>().bounds.size.x;
        wallcreator = GetComponent<CreateWall>();


        Vector3 frontWallPos = pos;
        frontWallPos.z += RoomWidth * VoxelSize;
        Vector3 leftWallPos = pos;
        leftWallPos.x += RoomLength * VoxelSize;
        Vector3 roofPos = pos;
        roofPos.y = WallHeight * VoxelSize;



        wallcreator.CreateHozWall(frontWallPos, voxel, RoomLength, WallHeight);
        wallcreator.CreateHozWall(pos, voxel, RoomLength, WallHeight);
        wallcreator.CreateWallFlipped(leftWallPos, voxel, RoomWidth, WallHeight);
        wallcreator.CreateWallFlipped(pos, voxel, RoomWidth, WallHeight);
        wallcreator.CreatePlane(pos, voxel, RoomLength, RoomWidth);
        wallcreator.CreatePlane(roofPos, voxel, RoomLength, RoomWidth);
        wallcreator.CombineMeshes();

    }
}
