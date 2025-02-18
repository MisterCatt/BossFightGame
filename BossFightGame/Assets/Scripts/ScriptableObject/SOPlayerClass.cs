using UnityEngine;

[CreateAssetMenu(fileName = "PlayerClass", menuName = "ScriptableObjects/PlayerClass")]
public class SOPlayerClass : ScriptableObject
{
    public Sprite previewSprite, HoverSprite, PrimaryAbilityImage, SecondaryAbilityImage, SpecialAbilityImage, MobilityAbilityImage;

    public int BasePrimaryDamage, BaseSecondaryDamage, BaseSpecialDamage;

    public string ClassName, PrimaryAbilityName, SecondaryAbilityName, SpecialAbilityName, MobilityAbilityName;

}
