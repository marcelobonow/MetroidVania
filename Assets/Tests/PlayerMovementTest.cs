using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovementTest
{

    [UnityTest]
    public IEnumerator TestPlayerIsEnabled()
    {
        yield return LoadSceneIfNotLoaded("Prototype");

        var player = GameObject.Find("Player");
        if (player == null)
            Assert.Fail("Player não achado");
        else if (player.gameObject.activeInHierarchy)
            Assert.Pass("Player ativo");
        else
            Assert.Fail("Player não esta ativo");
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestPlayerHasComponents()
    {
        yield return LoadSceneIfNotLoaded("Prototype");
        var player = GameObject.Find("Player");

        var animator = player.GetComponent<Animator>();
        var movementManager = player.GetComponent<MovementManager>();
        var rigidBody = player.GetComponent<Rigidbody2D>();
        var collider = player.GetComponent<Collider2D>();
        if (!animator)
            Assert.Fail("Player não tem animator");
        if (!movementManager)
            Assert.Fail("Player não tem movementManager");
        if (!rigidBody)
            Assert.Fail("Player não tem rigidBody");
        if (!collider)
            Assert.Fail("Player não tem collider");
    }

    [UnityTest]
    public IEnumerator TestPlayerHasComponentsActive()
    {
        yield return LoadSceneIfNotLoaded("Prototype");
        var player = GameObject.Find("Player");

        var animator = player.GetComponent<Animator>();
        var movementManager = player.GetComponent<MovementManager>();
        var rigidBody = player.GetComponent<Rigidbody2D>();
        var collider = player.GetComponent<Collider2D>();
        if (!animator.isActiveAndEnabled)
            Assert.Fail("Player não tem animator ativo");
        if (!movementManager.isActiveAndEnabled)
            Assert.Fail("Player não tem movementManager ativo");
        if (!rigidBody)
            Assert.Fail("Player não tem rigidBody");
        if (!collider.isActiveAndEnabled)
            Assert.Fail("Player não tem collider ativo");
    }

    [UnityTest]
    public IEnumerator AreComponentesSet()
    {
        yield return LoadSceneIfNotLoaded("Prototype");
        var player = GameObject.Find("Player");

        var animator = player.GetComponent<Animator>();
        var movementManager = player.GetComponent<MovementManager>();
        var rigidBody = player.GetComponent<Rigidbody2D>();
        var collider = player.GetComponent<Collider2D>();
        if (!animator.runtimeAnimatorController)
            Assert.Fail("Player não tem animator configurado");
        if (!movementManager.HasData())
            Assert.Fail("Player não tem movementManager configurado");
        if (rigidBody.gravityScale == 1)
            Assert.Fail("Player não tem rigidBody setado");
        if (collider.bounds.size == Vector3.one)
            Assert.Fail("Player não tem collider ativo");
    }

    private IEnumerator LoadSceneIfNotLoaded(string sceneName)
    {
        if (!IsSceneLoaded(sceneName))
            yield return SceneManager.LoadSceneAsync(sceneName);
    }

    private bool IsSceneLoaded(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }



}
