using UnityEngine;

public class BtnLog : MonoBehaviour
{
    
    public void TestCode()
    {
        Debug.Log($"{this.name} 버튼이 클릭됨");
    }
}
