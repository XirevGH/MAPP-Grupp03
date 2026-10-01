using System;
using System.Collections.Generic;

[Serializable]
public class MetaSaveData
{
    public int currency;
    public List<UpgradeSaveEntry> upgradeEntries = new List<UpgradeSaveEntry>();

    public int GetRank(string upgradeID)
    {
        var entry = upgradeEntries.Find(e => e.id == upgradeID);
        return entry != null ? entry.rank : 0;
    }

    public void SetRank(string upgradeID, int rank)
    {
        var entry = upgradeEntries.Find(e => e.id == upgradeID);
        if (entry != null)
        {
            entry.rank = rank;
        }
        else
        {
            upgradeEntries.Add(new UpgradeSaveEntry { id = upgradeID, rank = rank });
        }
    }
}

[Serializable]
public class UpgradeSaveEntry
{
    public string id;
    public int rank;
}