using _16TeamTextRPG;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static _16TeamTexTRPG.Skill;

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
            player = GameManager.Instance.player;

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

                warriorSkill.Add(new Skill("슬래시 블러스트", 1.5f, 5));
                warriorSkill.Add(new Skill("파워 스트라이크", 2f, 10));//리스트 데이터를 플레이어에게 보내서 계산 아니면 스킬 클래스에서 계산
            }
            public List<Skill> GetWarriorSkills()
            {
                return warriorSkill;
            }

        }

        public class ArcherSkill
        {
            public List<Skill> archerSkill;

            public ArcherSkill()
            {
                archerSkill = new List<Skill>();

                archerSkill.Add(new Skill("더블 샷", 1.3f, 10));
                archerSkill.Add(new Skill("애로우 블로우", 2.5f, 15));
            }
            public List<Skill> GetArcherSkills()
            {
                return archerSkill;
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
            public List<Skill> GetThiefSkills()
            {
                return thiefSkill;
            }
        }

        public class WizardSkill
        {
            public List<Skill> wizardSkill;
            public WizardSkill()
            {
                wizardSkill = new List<Skill>();

                wizardSkill.Add(new Skill("매직 클로", 2.0f, 18));
                wizardSkill.Add(new Skill("에너지 볼트", 2.5f, 20));
            }
            public List<Skill> GetWizardSkills()
            {
                return wizardSkill;
            }
        }


        public void SkillAttack(Monster monster)//캐릭터의 공격력 * 스킬배율 + 치명타 계산 or MP 계산
            //플레이어의 현재 직업
            //그직업에 맞게 스킬리스트를 스테이지 에게 보낸다? 플레이어에게 보내야하나?
            //플레이어가 선택한 스킬의 데이터를 다시 받는다
        {
            Random rand = new Random();
            
            if(player.mp < useMp)//현재 mp 보다 소모 mp가 많을시
            {
                Console.WriteLine("마나 가 부족합니다.");
                Console.WriteLine($"필요 MP : {useMp} 현재 MP : {player.mp}");// 필요 MP : 15, 현재 MP : 4
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
                Console.WriteLine($"HP {monster.maxHp} -> {monster.hp}"); // HP 100 -> 94
            }
            else
            {
                // 공격력의 10% 오차 (오차가 소수점이라면 올림처리);
                //int BaseDamage = player.atk; // 
                int min = player.Atk - (int)Math.Ceiling(player.atk * 0.1f);
                int max = player.Atk + (int)Math.Ceiling(player.atk * 0.1f);
                int BaseDamage = rand.Next(min, max + 1);
                float skillfinal = BaseDamage * skillsAtk; //선택한 스킬의 배율 계산

                // 최종 공격력
                float finalDamage = 0;
                bool isCriticalDamage = rand.Next(100) <= 15; // 15%의 확률로 크리티컬 데미지 발생
                bool isMissDamage = rand.Next(100) <= 20; // 20%의 확률로 회피 발생
                                                         
                int lastHp = monster.hp;

                //if (isMissDamage)
                //{
                //    Console.Clear();
                //    finalDamage = 0;
                //    Console.WriteLine($"{monster.name}(이)가 회피에 성공하였습니다다. [데미지 : {finalDamage}]");
                //}
                if (isCriticalDamage)
                {
                    finalDamage = (int)(skillfinal + (skillfinal * 1.6)); // 크리티컬이 발생하였을 때 적용되는 공식
                }
                else if (!isCriticalDamage)
                {
                    finalDamage = skillfinal; // 크리티컬이랑 회피가 발생하지 않을 경우 호출되는 함수수
                }

                //float criticalfinal = Critical(skillfinal);//치명타 계산이 끝난 데이터 보관
                float criticalfinal = skillfinal; // 치명타 계산

                player.mp -= useMp;//남은 마나 계산
                monster.hp -= (int)finalDamage;
                if (monster.hp <= 0)
                {
                    monster.dead = true;
                    monster.hp = 0;
                    GameManager.Instance.guild.UpdateProgress(0); // 몬스터 처치시 퀘스트 목표 업데이트
                }

                // Consol UI
                Console.WriteLine($"Lv.{player.level} {player.name}의 {skillName} 공격! [MP {player.maxMp} -> {player.mp}]"); // Lv.2 플레이어의 럭키 세븐 공격!
                Console.WriteLine($"{monster.name}을(를) 맞췄습니다. [데미지: {criticalfinal}]"); // 미니언 을(를) 맞췄습니다. [데미지: 6]

                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.level} {monster.name}"); // Lv.1 Chad
                Console.WriteLine($"HP {monster.maxHp} -> {monster.hp}"); // HP 100 -> 94
            }       
        }
    }
}
