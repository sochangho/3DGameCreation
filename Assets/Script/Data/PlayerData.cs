
using System.Collections.Generic;


[System.Serializable]
public class PlayerData 
{
    public int level;
    public int index;
    public int gold;
    public List<string> cardNames = new List<string>();
    public List<string> cardDackNames = new List<string>();
    public string towerName;
}





[System.Serializable]
public class OponentData
{
    public List<string> cardNames = new List<string>();
    public string towerName;

    
}

