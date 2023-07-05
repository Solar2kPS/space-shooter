﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartGameText;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Sprite[] lifeSprites;
    [SerializeField] private TMP_Text laserCount;
    [SerializeField] private Animator laserCountAnim;

    private GameManager gameManager;
    private Player player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        scoreText.text = "Score: " + 0;

        laserCount.text = player.GetAmmoCount().ToString();
        laserCountAnim.SetInteger("Laser Count", player.GetAmmoCount());
        gameOverText.enabled = false;
        restartGameText.enabled = false;

        if (player == null)
            Debug.LogError("The player is null.");
        if (gameManager == null)
            Debug.Log("The game manager is null.");
    }

    public void UpdateAmmoCount(int lasers) {
        laserCount.text = lasers.ToString();
        laserCountAnim.SetInteger("Laser Count", player.GetAmmoCount());
    }
    public void UpdateScore(int playerScore) {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateHealth(int currentLives) {
        if (currentLives < 0)
            currentLives = 0;
        lifeImage.sprite = lifeSprites[currentLives];
    }

    private void GameOverDisplay() {
        gameOverText.enabled = true;
        Assert.IsTrue(gameOverText.isActiveAndEnabled, "The Game Over text must be both " +
            "active and enabled for the text to show!");
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine() {
        while (true) {
            gameOverText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void RestartDisplay() {
        restartGameText.enabled = true;
        Assert.IsTrue(restartGameText.isActiveAndEnabled, "The Restart Game text must be "
            + "both active and enabled for the text to show!");
    }

    public void GameOverSequence() {
        gameManager.GameOver();
        GameOverDisplay();
        RestartDisplay();
    }
}