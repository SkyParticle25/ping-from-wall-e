using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Singleton <T> : MonoBehaviour where T: Singleton<T> 
{
    protected static T instance; 


    protected static void InitSingleton (T newInstance) 
    {
        if (instance != null) throw new UnityException("Instance of " + typeof(T) + " already exists"); 
        instance = newInstance; 
    }

    protected static void ClearSingleton () 
    {
        instance = null; 
    }
    
}
