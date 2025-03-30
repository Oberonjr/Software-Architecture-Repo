using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interface used for the enemies' TakeDamage
 * Implemented this way as there were original plans to expand on this mechanic
 * But this was later dropped, so it's an extra step for taking damage
 */
public interface IDamageable
{
    public void TakeDamage(int damage){}
}
