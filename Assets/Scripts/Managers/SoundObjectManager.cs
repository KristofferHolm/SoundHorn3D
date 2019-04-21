using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectManager : MonoBehaviour
{
    public List<SoundCategory> Categories;
    private static SoundObjectManager instance;
    public static SoundObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundObjectManager>();
            }
            return instance;
        }
    }

    public bool CheckCategoryForSound(AudioClip clip, SoundObjectCategory category)
    {
        return GetSoundsFromCategory(category).Contains(clip);
    }
    public List<AudioClip> GetSoundsFromCategory(SoundObjectCategory category)
    {
        foreach (var cat in Categories)
        {
            if (cat.Category == category)
                return cat.Sounds;
        }
        return null;
    }
    public SoundObjectCategory GetCategory(AudioClip clip)
    {
        foreach (var category in Categories)
        {
            foreach (var sound in category.Sounds)
            {
                if (sound.Equals(clip))
                    return category.Category;
            }
        }
        return SoundObjectCategory.none;
    }


    
}

[Serializable]
public class SoundCategory
{
    public SoundObjectCategory Category;
    public List<AudioClip> Sounds;
}
public enum SoundObjectCategory
{
    ticks,
    friction,
    open,
    close,
    pitch,
    tempo,
    continuous,
    none
}