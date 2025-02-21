using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaveController : MonoBehaviour
{
    [field: SerializeField]
    public int EnemyShipsRemaining { get; private set; } = 0;

    [field: SerializeField]
    public int Wave { get; private set; } = 1;

    [field: SerializeField]
    private List<EnemyShip> PossibleEnemies { get; set; }

    [field: SerializeField]
    private EnemySpawner Spawner { get; set; }

    [field: SerializeField]
    public UnityEvent<int> OnWaveCompleted { get; private set; }

    [field: SerializeField]
    public UnityEvent<int> OnWaveStart { get; private set; }

    private void Start()
    {
        Spawner.OnSpawn.AddListener(RegisterEnemy);
    }

    private void RegisterEnemy(EnemyShipController enemy)
    {
        var destructable = enemy.GetComponent<DestructableController>();
        if (destructable != null)
        {
            // Tránh đăng ký trùng lặp
            destructable.OnDestroyed.RemoveListener(HandleEnemyDestroyed);
            destructable.OnDestroyed.AddListener(HandleEnemyDestroyed);

            Debug.Log($"📌 Đăng ký kẻ địch: {enemy.name}");
        }
        else
        {
            Debug.LogError($"⚠️ Kẻ địch {enemy.name} không có DestructableController!");
        }
    }

    private void HandleEnemyDestroyed(DestructableController destructable)
    {
        if (EnemyShipsRemaining <= 0)
        {
            Debug.LogWarning("⚠️ Lỗi: Gọi HandleEnemyDestroyed nhưng không còn kẻ địch!");
            return;
        }

        EnemyShipsRemaining--;
        Debug.Log($"💥 Kẻ địch bị tiêu diệt! Còn lại: {EnemyShipsRemaining} (Wave {Wave})");

        if (EnemyShipsRemaining <= 0)
        {
            Debug.Log($"🎉 Đã tiêu diệt toàn bộ kẻ địch! Chuẩn bị sang Wave {Wave + 1}");
            OnWaveCompleted.Invoke(Wave);
            Wave++;
            Invoke(nameof(StartWave), 3);
            Debug.Log("⏳ Chờ 3 giây để bắt đầu Wave mới...");
        }
    }

    public void Reset()
    {
        Wave = 1;
        Spawner.SpawnRate = 3;
        Debug.Log("🔄 Reset game. Quay lại Wave 1!");
        Invoke(nameof(StartWave), 3);
    }

    private void StartWave()
    {
        List<EnemyShip> enemies = FilterEnemies(Wave);

        if (enemies.Count == 0)
        {
            Debug.LogError($"⚠️ Không tìm thấy kẻ địch hợp lệ cho Wave {Wave}. Trò chơi có thể bị kẹt!");
            return;
        }

        EnemyShipsRemaining = 3 * Wave;

        for (int i = 0; i < EnemyShipsRemaining; i++)
        {
            int randomIx = Random.Range(0, enemies.Count);
            EnemyShip e = enemies[randomIx];
            Spawner.EnqueEnemy(e);
        }

        OnWaveStart.Invoke(Wave);

        float diff = Mathf.Clamp(Mathf.Log10(10 * Wave), 0, 2.5f) - 1;
        Spawner.SpawnRate = Mathf.Max(3 - diff, 0.5f);

        Debug.Log($"🚀 Wave {Wave} bắt đầu! Số lượng địch: {EnemyShipsRemaining}, Tốc độ spawn: {Spawner.SpawnRate}");
    }

    private List<EnemyShip> FilterEnemies(int maxWave)
    {
        List<EnemyShip> enemies = new();
        foreach (EnemyShip e in PossibleEnemies)
        {
            if (e.WaveDifficulty <= maxWave)
            {
                enemies.Add(e);
            }
        }

        Debug.Log($"🧐 Lọc kẻ địch cho Wave {maxWave}. Số lượng tìm thấy: {enemies.Count}");
        return enemies;
    }
}
