using _16TeamTextRPG;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _16TeamTexTRPG
{
    public class Skill
    {
        /*
        스테이지랑 플레이어 연동
        치명타 랑 연동
        */
        public Player player;

        public string skillName { get; set; } //스킬 이름
        public float skillsAtk { get; set; } //공격력 배율
        public int useMp { get; set; } //사용 mp

        public Skill(string name, float skillsatk, int usemp)
        {

            skillName = name;
            skillsAtk = skillsatk;
            useMp = usemp;
        }
        
        public class WarriorSkill
        {
            public List<Skill> warriorSkill;

            public WarriorSkill()
            {
                warriorSkill = new List<Skill>();

                warriorSkill.Add(new Skill("파워 스트라이크", 2f, 10));//리스트 데이터를 플레이어에게 보내서 계산 아니면 스킬 클래스에서 계산
                warriorSkill.Add(new Skill("슬래시 블러스트", 1.5f, 5));
            }
            
        }

        public class ArcherSkill
        {
            public List<Skill> archerSkill;

            public ArcherSkill()
            {
                archerSkill = new List<Skill>();

                archerSkill.Add(new Skill("애로우 블로우", 2.5f, 15));
                archerSkill.Add(new Skill("더블 샷", 1.3f, 10));
            }
        }

        public class ThiefSkill
        {
            public List<Skill> thiefSkill;

            public ThiefSkill()
            {
                thiefSkill = new List<Skill>();

                thiefSkill.Add(new Skill("더블 스탭", 1.5f, 10));
                thiefSkill.Add(new Skill("럭키 세븐", 2.0f, 15));
            }
        }

        public class WizardSkill
        {
            public List<Skill> wizardSkill;
            public WizardSkill()
            {
                wizardSkill = new List<Skill>();

                wizardSkill.Add(new Skill("에너지 볼트", 2.5f, 20));
                wizardSkill.Add(new Skill("매직 클로", 2.0f, 18));
            }
        }


        public void SkillAttack(Player currentplyer, Monster monster , Skill skilldam)//캐릭터의 공격력 * 스킬배율 + 치명타 계산 or MP 계산
            //플레이어의 현재 직업
            //그직업에 맞게 스킬리스트를 스테이지 에게 보낸다
            //플레이어가 선택한 스킬의 데이터를 다시 받는다
        {

            Random rand = new Random();
            
            if(currentplyer.mp < skilldam.useMp)//현재 mp 보다 소모 mp가 많을시
            {
                Console.WriteLine("마나 가 부족합니다.");
                Console.WriteLine($"필요 MP : {skilldam.useMp} 현재 MP : {currentplyer.mp}");// 필요 MP : 15, 현재 MP : 4
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
                Console.WriteLine($"HP {monster.maxHp} -> {monster.hp}"); // HP 100 -> 94
            }
            else
            {

                // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
                int min = currentplyer.atk - (int)Math.Ceiling(currentplyer.atk * 0.1f);
                int max = currentplyer.atk + (int)Math.Ceiling(currentplyer.atk * 0.1f);

                // 최종 공격력
                int final = rand.Next(min, max + 1);
                float skillfinal = final * skilldam.skillsAtk; //선택한 스킬의 배율

                float criticalfinal = Critical(skillfinal);//치명타 계산이 끝난 데이터 보관

                currentplyer.mp -= skilldam.useMp;//남은 마나 계산
                monster.hp -= final;
                if (monster.hp <= 0)
                {
                    monster.dead = true;
                    monster.hp = 0;
                }
                // Consol UI
                Console.WriteLine($"Lv.{player.level} {player.name}의 {skillName} 공격!"); // Lv.2 플레이어의 럭키 세븐 공격!
                Console.WriteLine($"{monster.name}을(를) 맞췄습니다. [데미지: {criticalfinal}]\n"); // 미니언 을(를) 맞췄습니다. [데미지: 6]
                Console.WriteLine();
                Console.WriteLine($"MP {currentplyer.maxMp} -> {currentplyer.mp}");// MP 50 -> 45

                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
                Console.WriteLine($"HP {monster.maxHp} -> {monster.hp}"); // HP 100 -> 94
            }       
        }
    }
}
