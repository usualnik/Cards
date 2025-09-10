using UnityEngine;
using UnityEngine.UI;
using YG;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite[] _ruTutorialSprites;
    [SerializeField] private Sprite[] _enTutorialSprites;

    [SerializeField] private GameObject[] _cardsHighlighted;
    [SerializeField] private GameObject _boxHighlighted;
    [SerializeField] private GameObject _bufferHighlighted;
 
    private const int TUTORIAL_STEPS_MAX = 4;
    private int _tutorialStep = 0;
    private Image _tutorialImage;
    private bool _ruTutorial = false;
    private const int DEFAULT_RENDER_LAYER = 0;
    private const int HIGHLIGHTED_RENDER_LAYER = 6;



    private void Awake()
    {
        _tutorialImage = GetComponentInChildren<Image>();
    }

    void Start()
    {
        _ruTutorial = YG2.envir.language == "ru" ? true : false;
        
        _tutorialImage.sprite = _ruTutorial ? _ruTutorialSprites[0]
               : _enTutorialSprites[0];

    }
    
    void Update()
    {
       CheckForInput();

        CheckWhatsNeedToBeHighlighted();       

        if (_tutorialStep >= TUTORIAL_STEPS_MAX)
        {
            Destroy(gameObject);
        }
        
    }

    private void CheckForInput()
    {
        if (Input.anyKeyDown && _tutorialStep < TUTORIAL_STEPS_MAX)
        {
            _tutorialStep++;

            if (_tutorialStep < _ruTutorialSprites.Length ||
                _tutorialStep < _enTutorialSprites.Length)
            {
                _tutorialImage.sprite = _ruTutorial ? _ruTutorialSprites[_tutorialStep]
                : _enTutorialSprites[_tutorialStep];
            }
            else
            {
                return;
            }

        }
        else
        {
            return;
        }
      
    }

    private void CheckWhatsNeedToBeHighlighted()
    {
        var boxVisual = _boxHighlighted.GetComponentInChildren<MeshRenderer>();
        var bufferVisual = _bufferHighlighted.GetComponentInChildren<MeshRenderer>();


        switch (_tutorialStep)
        {
            case 1:

                foreach (var card in _cardsHighlighted)
                {
                    var cardVisuals = card.GetComponentsInChildren<MeshRenderer>();
                    foreach (var cardVisual in cardVisuals)
                    {
                        cardVisual.gameObject.layer = 0;
                    }
                }

                boxVisual.gameObject.layer = HIGHLIGHTED_RENDER_LAYER;

                break;

            case 2:
                boxVisual.gameObject.layer = DEFAULT_RENDER_LAYER;
                bufferVisual.gameObject.layer = HIGHLIGHTED_RENDER_LAYER;

                break;

            default:
                break;
        }
    }

}
