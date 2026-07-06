using UnityEngine;
using UnityEngine.Assertions;

namespace Febucci.UI.Examples
{
    /// <summary>
    /// Extra example class for the TextAnimator plugin, used to add sounds to the TextAnimatorPlayer.
    /// </summary>
    [AddComponentMenu("Febucci/TextAnimator/TextSoundWriter")]
    [RequireComponent(typeof(Core.TypewriterCore))]
    public class TextSoundWriter : MonoBehaviour
    {

        [Header("References")]
        public AudioSource source;

        [Header("Management")]
        [Tooltip("How much time has to pass before playing the next sound"), SerializeField, Attributes.MinValue(0)]
        float minSoundDelay = .07f;

        [Tooltip("True if you want the new sound to cut the previous one\nFalse if each sound will continue until its end"), SerializeField]
        bool interruptPreviousSound = true;

        [Header("Audio Clips")]
        [Tooltip("True if sounds will be picked random from the array\nFalse if they'll be chosen in order"), SerializeField]
        bool randomSequence = false;
        [SerializeField] AudioClip[] sounds = new AudioClip[0];

        float latestTimePlayed = -1;
        int clipIndex;

        private void Awake()
        {
            
        }
        private void OnEnable()
        {
            //Assert.IsNotNull(source, "TAnimSoundWriter: Typewriter Audio Source reference is null");
            //Assert.IsNotNull(sounds, "TAnimSoundWriter: Sounds clips array is null in the");
            //Assert.IsTrue(sounds.Length > 0, "TAnimSoundWriter: Sounds clips array is empty");
            //Assert.IsNotNull(GetComponent<Core.TypewriterCore>(), "TAnimSoundWriter: Component TAnimPlayerBase is not present");


            //Prevents subscribing the event if the component has not been set correctly
            if (this.source == null || this.sounds.Length <= 0)
                return;

            //Prevents common setup errors
            this.source.playOnAwake = false;
            this.source.loop = false;

            GetComponent<Core.TypewriterCore>()?.onCharacterVisible.AddListener(this.OnCharacter);

            this.clipIndex = this.randomSequence ? Random.Range(0, this.sounds.Length) : 0;
        }

        void OnCharacter(char character)
        {
            System.String str = " ";
            char c = str[0];
            //make it so it doesnt play for line break too
            if (character != c)
            {
                //Debug.Log("test");
                if (Time.time - this.latestTimePlayed <= this.minSoundDelay)
                    return; //Early return if not enough time passed yet

                this.source.clip = this.sounds[this.clipIndex];

                //Plays audio
                if (this.interruptPreviousSound)
                    this.source.Play();
                else
                    this.source.PlayOneShot(this.source.clip);

                //Chooses next clip to play
                if (this.randomSequence)
                {
                    this.clipIndex = Random.Range(0, this.sounds.Length);
                }
                else
                {
                    this.clipIndex++;
                    if (this.clipIndex >= this.sounds.Length)
                        this.clipIndex = 0;
                }

                this.latestTimePlayed = Time.time;
            }


        }
    }
}