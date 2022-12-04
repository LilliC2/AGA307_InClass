using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioClip[] enemyHitSounds; //audio clip is the sound source itself

    public AudioClip[] enemyDieSounds; //even if we only have one it makes it more flexible if we want to add more
    public AudioClip[] footstepsSounds;

    public AudioClip GetEnemyHitSound()
    {
        return enemyHitSounds[Random.Range(0, enemyHitSounds.Length)];
    }


    public AudioClip GetEnemyDieSound()
    {
        return enemyDieSounds[Random.Range(0, enemyDieSounds.Length)];
    }

    public AudioClip GetFootsteps()
    {
        return footstepsSounds[Random.Range(0, footstepsSounds.Length)];
    }






}
