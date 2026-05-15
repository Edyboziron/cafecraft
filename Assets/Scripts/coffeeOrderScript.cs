using UnityEngine;
using UnityEngine.SceneManagement;

public class coffeeOrderScript : MonoBehaviour
{
   
  

    public static  coffeeOrderScript coffeeMaker;
   
 

    public  int level1Coffee;
    public  int level2Coffee;
    public  int level3Coffee;
    public  int level4Coffee;
    public  int level5Coffee;
    public  int level6Coffee;
    public  int level7Coffee;
    public  int level8Coffee;
    public  int level9Coffee;


        private void Awake()
    {
        transform.SetParent(null);
        if ( coffeeMaker == null )
        {   
            coffeeMaker =this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }






        

      
         



   



}
