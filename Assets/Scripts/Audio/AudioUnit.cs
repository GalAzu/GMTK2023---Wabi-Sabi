
using UnityEngine;

namespace _AudioStuff
{
    public enum SeqToPlay
    {
        SpiritBomb,
        Sharingan,
        RagingDemon,
        zeeku,
        EnemyHaste,
    }
    public enum UISfxToPlay
    {
        StartGame,
        SuccessfulClick,
        UnsuccessfulClick,
        MoveOnMenu,
        OnStartGame,
        LevelUp,
        MenuBack,
        OnPerkGet,
        CrosshairMovement,
    }
    public enum SfxToPlay
    {
        explosion,
        gainOrb,
        gunshot,
        LaserBeam,
        spreadShot,
        placeTower,
        homeDamage,
        addOnWeapon,
        droneAttack,
        LazorOneShot,
        OnTowerActions,
        attackingOrb,
        SlowNova,
        SwordExpansion,
        DeathWave,
        RagingDemonHit
    }
    [System.Serializable]
    public class AudioUnit
    {
        public string name;
        [Range(-80, 4)]
        public float volume = 1;
        public AudioClip clip;
        public float minDistance = 8;
        public int maxDistance = 25;
        public int maxInstances = 1;
    }
    [System.Serializable]

    public class AudioUnitBGM
    {
        public string name;
        public int bpm;
        public AudioClip[] clips;
    }

    [System.Serializable]
    public class AudioUnitSFX : AudioUnit
    {
        public bool randomPitch;
        public SfxToPlay sfxToPlay;
    }

    [System.Serializable]
    public class AudioUnitUI : AudioUnit
    {
        public bool isPooled;
        public UISfxToPlay uiSfxToPlay;
    }
    [System.Serializable]
    public class AudioUnitSFXSequence
    {
        public int volume = 1;
        public int maxInstances = 1;
        public float minDistance = 8;

        public float maxDistance = 25;

        public string sequenceName;
        public SeqToPlay SequenceToPlay;
        public AudioUnit[] sfxSequence;
    }

}









