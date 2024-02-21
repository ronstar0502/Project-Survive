
using UnityEngine;

public class ExperienceObject : MonoBehaviour
{
    public int experienceValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && this.tag == "expObj")
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.AddEXP(experienceValue);
            Destroy(this.gameObject);
        }
    }

}
