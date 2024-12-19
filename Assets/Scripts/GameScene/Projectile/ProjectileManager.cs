using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    public List<IProjectile> projectiles = new List<IProjectile>();
    
    [SerializeField] private Transform projectileParent;
    
    [SerializeField] private List<AstroDataSO> astroDataList = new List<AstroDataSO>();
    [SerializeField] private int nextProjectileIndex;

    [SerializeField] private float maxDistanceToCenter;

    [SerializeField] private GameObject AstroBasicPrefab;

    private float projectileOutOfRadiusTimer = 0.0f;
    private float gameOverTime = 1.0f;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        UpdateProjectileData();
        if(CheckGameOver())
        {
            Managers.Game.GameOver();
        }
    }


    public int GetTotalScore()
    {
        int totalScore = 0;
        foreach(var item in projectiles)
        {
            if(item.CheckAvailability())
            {
                totalScore += item.GetScore();
            }
        }
        return totalScore;
    }

    private void UpdateProjectileData()
    {

        UpdateMaxDistAndSProjectiletate();
    }

    private bool CheckGameOver()
    {
        if(CheckProjectileOutOfRadius())
        {
            projectileOutOfRadiusTimer += Time.deltaTime;
            if(projectileOutOfRadiusTimer > gameOverTime)
            {
                projectileOutOfRadiusTimer = 0.0f;
                return true;
            }
        }
        else
        {
            projectileOutOfRadiusTimer = 0f;
        }
        return false;
    }

    public bool CheckProjectileOutOfRadius()
    {
        if(projectiles.Count != 0)
        {
            if (maxDistanceToCenter > GravityZone.GravityZoneRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void UpdateMaxDistAndSProjectiletate()
    {
        float maxDist = float.MinValue;
        foreach (var projectile in projectiles)
        {
            float distance = Vector2.Distance(projectile.GetCenterPos(), GravityZone.CenterPos);
            float radius = projectile.GetRadius();

            float totalDistance = distance + (radius * 0.43f);
            //float totalDistance = distance;

            if (projectile.GetActivationStatus())
            {
                if (totalDistance > maxDist)
                {
                    maxDist = totalDistance;
                }
            }
        }
        //Debug.Log("maxDist = " + maxDist + " / Radius = " + GravityZone.GravityZoneRadius);
        maxDistanceToCenter = maxDist;

    }

    



    /// <summary>
    /// 해당 IProjectile을 List에 저장시킨 뒤에, 해당 오브젝트의 인스턴스를 넘김.
    /// </summary>
    /// <returns></returns>
    public GameObject GetNewProjectile(Vector3 position)
    {
        int index = nextProjectileIndex; 
        GameObject instantiatedProjectile = Instantiate(AstroBasicPrefab);
        instantiatedProjectile.GetComponent<ProjectileAstro>().RefreshAstro(astroDataList[index]);
        instantiatedProjectile.transform.parent = projectileParent;
        instantiatedProjectile.transform.position = position;
        instantiatedProjectile.transform.rotation = Quaternion.identity;
        projectiles.Add(instantiatedProjectile.GetComponent<IProjectile>());

        GetNextAstroIndex();

        return instantiatedProjectile;
    }

    public AstroDataSO GetNextProjectileSO()
    {
        return astroDataList[nextProjectileIndex];
    }

    public bool RemoveFromList(IProjectile projectile)
    {
        if(projectiles.Contains(projectile))
        {
            projectiles.Remove(projectile);
            
            return true;
        }
        return false;
    }

    public static bool CheckIsInsideGravityZone(Vector2 position)
    {
        float distance = Vector2.Distance(position, GravityZone.CenterPos);
        if(distance < GravityZone.GravityZoneRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GetNextAstroIndex()
    {
        nextProjectileIndex = Random.Range(0, astroDataList.Count);
    }

    public float GetMaxDistanceToCenter()
    {
        return maxDistanceToCenter;
    }

    public float GetMaxDistanceInActiveProjectile()
    {
        return maxDistanceToCenter;
    }





}
