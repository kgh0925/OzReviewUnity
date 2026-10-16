using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponExample : MonoBehaviour
{
    public struct NormalWeapon
    {
        public string name;
        public int power;
    }
    public class LegendaryWeapon
    {
        public string name;
        public int power;
    }

    public struct BaseStats
    {
        public int hp;
        public int atk;
    }

    public class SkillSet
    {
        public string skillName;
        public float multiplier;
    }

    public class  MonsterTemplate
    {
        public BaseStats stats;
        public List<SkillSet> skills = new List<SkillSet>();

        public MonsterTemplate Clone()
        {
            MonsterTemplate clone = new MonsterTemplate();
            clone.stats = stats;

            foreach (SkillSet skill in skills)
            {
                SkillSet copiedSkill = new SkillSet();
                copiedSkill.skillName = skill.skillName;
                copiedSkill.multiplier = skill.multiplier;

                clone.skills.Add(copiedSkill);
            }

            return clone;
        }
    }
    NormalWeapon original = new NormalWeapon();
    NormalWeapon copy;
    LegendaryWeapon Loriginal = new LegendaryWeapon();
    LegendaryWeapon Lcopy;

    MonsterTemplate dragon = new MonsterTemplate();
    MonsterTemplate temp;

    void Start()
    {
        original.power = 10;
        copy = original;
        Loriginal.power = 100;
        Lcopy = Loriginal;

        dragon.stats.hp = 1000;
        dragon.stats.atk = 100;
        SkillSet tempskill = new SkillSet();
        tempskill.multiplier = 1.5f;
        dragon.skills.Add(tempskill);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            copy.power = 50;
            Debug.Log("일반 등급 복사본의 공격력을 50으로 수정");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Lcopy.power = 999;
            Debug.Log("전설 등급 복사본의 공격력을 999으로 수정");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Lcopy = null;
            Debug.Log("전설 등급 복사본 연결 해제");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log($"일반 등급 오리지널 공격력 : {original.power} | 일반 등급 복사본 공격력 {copy.power}");
            Debug.Log($"전설 등급 오리지널 공격력 : {Loriginal.power} | 전설 등급 복사본 공격력 {Lcopy.power}");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            temp = dragon.Clone();
            temp.stats.hp = 500;
            temp.skills[0].multiplier = 9.9f;
            Debug.Log("드래곤 복사 완료[ 체력 500으로 조정, 스킬 배수 9.9로 수정 ]");
            Debug.Log("===== 원본 드래곤 =====");
            MonsterInformation(dragon);
            Debug.Log("======================");
            Debug.Log("===== 클론 드래곤 =====");
            MonsterInformation(temp);
            Debug.Log("======================");
        }

    }

    private void MonsterInformation(MonsterTemplate monster)
    {
        Debug.Log($"몬스터의 체력 {monster.stats.hp}, 공격력 {monster.stats.atk}, 1번 스킬 배율 : {monster.skills[0].multiplier}");
    }
}
