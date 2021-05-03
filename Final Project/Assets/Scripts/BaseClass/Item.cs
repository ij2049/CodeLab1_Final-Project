using UnityEngine;

//this remark the class realizable that we can show this value on our inspector
//it means the Unity can Serialize this class?
// 클래스로 생성후 그걸 인스펙터에 보여줌. 그리고 거기서 받은 정보를 저장하는것같음.
// not public but it shows on the inspector
[System.Serializable]

public class Item
{
  [SerializeField] private int itemId;
  [SerializeField] private string itemName;
  [SerializeField] private string itemDescription;
  [SerializeField] private Sprite itemSprite;
  [SerializeField] private bool allowMultiple;
  [SerializeField] private int amount;

  public Item(int itemId, string name, string desc)
  {
    this.itemId = itemId;
    itemName = name;
    itemDescription = desc;
  }
}
