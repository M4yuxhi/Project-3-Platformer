using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : ButtonsMenu
{
    [SerializeField] private GameObject howToPlayParent;
    private bool howToPlayShowing = false;

    protected override void Start()
    {
        base.Start();    
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.H))
        {
            howToPlayShowing = !howToPlayShowing;
            howToPlayParent.SetActive(howToPlayShowing);
        }
    }

    protected override void DoAction(int selectedOption)
    {
        if (howToPlayShowing) return;

        Globals.SetPause(false);
        gameObject.SetActive(false);

        switch (selectedOption)
        {
            case 1 :

                SceneManager.LoadScene("GameScene");

                break;

            case 2 :

                SceneManager.LoadScene("MainScene");

                break;
        }
    }

    public override void Navegate(int min, int max)
    {
        if (howToPlayShowing) return;
        base.Navegate(min, max);
    }

    public void SetActiveButtons(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(value);
        }
    }

    public void SetActiveHowToPlayParent(bool value)
    {
        howToPlayParent.SetActive(value);
        howToPlayShowing = value;
    }
}
