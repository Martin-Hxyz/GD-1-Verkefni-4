using System.Linq;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public GameObject reward;
    private int _lastCount = -1;
    private float _checkTimer = 0;

    // Tjekka hversu margir róbotar eru ólagaðir
    // Þegar þessi tala fer niður í 0 er 'reward' GameObjectið activeitað
    void FixedUpdate()
    {
        if (_checkTimer > 0)
        {
            _checkTimer -= Time.deltaTime;
            return;
        }

        int counted = CountRobots();

        if (counted != _lastCount)
        {
            _lastCount = counted;
            _checkTimer = 3.0f;
        }


        if (counted <= 0)
        {
            reward.SetActive(true);
            enabled = false;
        }
    }

    
    // flott leið til að filtera og telja með LINQ
    public int CountRobots()
    {
        var robots = FindObjectsOfType<RobotController>();
        return robots.Count(robot => robot.IsBroken());
    }
}