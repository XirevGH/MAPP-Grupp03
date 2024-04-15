using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PositionGrups : MonoBehaviour
{

//seperata metoder som refrens för att ta bort coliders

  /*int GetSpatialGroup(float xPos, float yPos, float mapWith, float mapHight, int totalPartiions){

    int cellsPerRow = (int)Mathf.Sqrt(totalPartiions);
    int cellsPerColumn = cellsPerRow;

    float cellWidth = mapWith / cellsPerRow;
    float cellHeight = mapHight / cellsPerRow;


    float adjustX = xPos + (mapWith / 2);
    float adjustY = yPos + (mapHight / 2);

    int xIndex = (int)(adjustX / cellWidth);
    int yIndex = (int)(adjustY / cellWidth);

    xIndex = Mathf.Clamp(xIndex, 0, cellsPerRow - 1);
    yIndex = Mathf.Clamp(yIndex, 0, cellsPerColumn - 1);

    return xIndex + yIndex * cellsPerRow;
  }

    //enemy script
  public void RunLogic(){

    movmentDirection = GameController.Instantiate.position - transform.position;
    movmentDirection.Normalize();

    transform.position += movmentDirection * Time.deltaTime* movementSpeed;

    PushNerbyEnemies();

    int newSpatialGroup = GameController.Instantiate.GetSpatialGroup(transform.position.x , transform.Position.y);
    if(newSpatialGroup != spatialGroup){

        GameController.Instantiate.enemySpatialGroups[spatialGroup].remove(this);

        spatialGroup = newSpatialGroup;
        GameController.Instantiate.enemySpatialGroups[spatialGroup].Add(this);
    }
    
  }

  void PushNerbyEnemies(){
    List<Enemy> currAreaEnemies = GameController.Instantiate.enemySpatialGroups[spatialGroup].ToList();
    foreach(Enemy enemy in currAreaEnemies){
        if(enemy == null) continue;
        if(enemy == this) continue;

        float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x) + (transform.position.y - enemy.transform.position.y);

        if(distance < 0.2f){
            Vector3 direction = transform.position - enemy.transform.position;
            direction.Normalize();
            enemy.transform.position -= direction * Time.deltaTime * movmentSpeed * 5;
        }

    }
  }*/
}
