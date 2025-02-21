using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableController))]
public class AsteroidController : MonoBehaviour
{
    [field: SerializeField]
    public AsteroidController OnDestroyedTemplate { get; private set; }
    [field: SerializeField]
    public float RotationSpeed { get; private set; }
    [field: SerializeField]
    public Vector2 Speed { get; private set; }
    public GameController GameController { get; private set; }

    // Hàm Spawn với random hướng đi
    public static AsteroidController Spawn(AsteroidController template, float rotationSpeed, float minSpeed, float maxSpeed, GameController gameController)
    {
        AsteroidController newAsteroid = Instantiate(template);
        newAsteroid.GameController = gameController;
        newAsteroid.GetComponent<DestructableController>().GameController = gameController;
        newAsteroid.RotationSpeed = rotationSpeed;

        // Random hướng đi
        newAsteroid.Speed = GenerateRandomSpeed(minSpeed, maxSpeed);

        return newAsteroid;
    }

    // Tạo vector Speed ngẫu nhiên
    private static Vector2 GenerateRandomSpeed(float minSpeed, float maxSpeed)
    {
        // Random giá trị tốc độ trong khoảng minSpeed đến maxSpeed
        float speedX = Random.Range(minSpeed, maxSpeed);
        float speedY = Random.Range(minSpeed, maxSpeed);

        // Random dấu để thay đổi hướng (âm hoặc dương)
        speedX *= Random.value > 0.5f ? 1 : -1;
        speedY *= Random.value > 0.5f ? 1 : -1;

        // Nếu muốn thiên thạch có thể bay thẳng (chỉ x hoặc y), thêm logic chọn ngẫu nhiên
        int directionChoice = Random.Range(0, 3); // 0: chỉ x, 1: chỉ y, 2: cả x và y (chéo)
        switch (directionChoice)
        {
            case 0: // Chỉ di chuyển ngang
                return new Vector2(speedX, 0);
            case 1: // Chỉ di chuyển dọc
                return new Vector2(0, speedY);
            default: // Di chuyển chéo
                return new Vector2(speedX, speedY);
        }
    }

    void Update()
    {
        RotateMeteor();
        MoveMeteor();
    }

    public void OnLaserHit(LaserController laser, DestructableController destructable)
    {
        if (OnDestroyedTemplate != null)
        {
            AsteroidController newObj = Spawn(OnDestroyedTemplate, RotationSpeed, 1f, 2f, GameController); // Ví dụ minSpeed = 1, maxSpeed = 2
            newObj.transform.position = this.transform.position;
        }
        destructable.DefaultDestroy(laser);
    }

    private void RotateMeteor()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new(0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);
    }

    public void MoveMeteor()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}