
using System.Collections.Generic;

public class ParameterHelper 
{
    public List<object> objList = new List<object>();


    public T GetParameter<T>() where T : class
    {
      
        for(int i = 0; i < objList.Count; i++)
        {
            if(objList[i] is T)
            {
                T parameter = (T)(objList[i]);
                return parameter;
            }


        }

        return null;
    }


}
