//******************************************************************************************************
//  Copyright © 2022, S. Christison. No Rights Reserved.
//
//  Licensed to [You] under one or more License Agreements.
//
//      http://www.opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
//******************************************************************************************************

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoopDelay.NET
{
    /// <summary>
    /// Extended Loops
    /// <para>while (Loop.Run(startTime, TimeSpan.FromMilliseconds(7), TimeSpan.FromSeconds(2)).Result) { }</para>
    /// <para>while (await Loop.Run(startTime, TimeSpan.FromMilliseconds(7), TimeSpan.FromSeconds(2))) { }</para>
    /// </summary>
    public class Loop
    {
        /// <summary>
        /// Loop and Delay - Return at the Expire Time
        /// <para>Returns bool indicating if the loop should continue</para>
        /// <para>Strongly Typed, Slower</para>
        /// </summary>
        /// <param name="startTime">The <see cref="DateTime">DateTime</see> the Loop started</param>
        /// <param name="delayTimeSpan">A <see cref="TimeSpan">TimeSpan</see> that represents the Delay</param>
        /// <param name="expireTimeSpan">A <see cref="TimeSpan">TimeSpan</see> that represents the Expire Time</param>
        /// <returns>True if Loop should Continue; False if it should Stop</returns>
        public static async Task<bool> Delay(DateTime startTime, TimeSpan delayTimeSpan, TimeSpan expireTimeSpan)
        {
            // Wait Delay
            await Task.Delay(delayTimeSpan).ConfigureAwait(false);
            TimeSpan t = startTime - DateTime.UtcNow;

            // Expire (avoids any infinite loop)
            if (t > expireTimeSpan)
            {
#if DEBUG
                Debug.WriteLine("Expired: " + t);
#endif
                // Return and Stop
                return false;
            }
#if DEBUG
            Debug.WriteLine("Continue: " + t);
#endif
            // Return and Continue
            return true;
        }

        /// <summary>
        /// Loop and Delay - Return at the Expire Time
        /// <para>Returns bool indicating if the loop should continue</para>
        /// <para>There are 10,000 Ticks in a millisecond</para>
        /// <para>Simple, Faster</para>
        /// </summary>
        /// <param name="startTicks">When the loop started in "Ticks"</param>
        /// <param name="delayMs">Delay in Milliseconds</param>
        /// <param name="expireMs">How long to wait before the Loop indicates it should Stop</param>
        /// <returns>True if Loop should Continue; False if it should Stop</returns>
        public static async Task<bool> Delay(long startTicks, int delayMs, int expireMs)
        {
            // Wait Delay
            long t = DateTime.UtcNow.Ticks - startTicks;
            await Task.Delay(delayMs).ConfigureAwait(false);

            // Expire (avoids any infinite loop)
            if (t > (expireMs * 10000))
            {
#if DEBUG
                Console.WriteLine("Expired: " + t);
#endif
                // Return and Stop
                return false;
            }
#if DEBUG
            Console.WriteLine("Continue: " + t);
#endif
            // Return and Continue
            return true;
        }

        /// <summary>
        /// Loop and Delay - Run an Action and Return at the Expire Time
        /// <para>Returns bool indicating if the loop should continue</para>
        /// <para>Runs an Action only when the Loop Expires, not if you manually break the Loop</para>
        /// <para>Strongly Typed, Slower</para>
        /// </summary>
        /// <param name="startTime">The <see cref="DateTime">DateTime</see> the Loop started</param>
        /// <param name="delayTimeSpan">A <see cref="TimeSpan">TimeSpan</see> that represents the Delay</param>
        /// <param name="expireTimeSpan">A <see cref="TimeSpan">TimeSpan</see> that represents the Expire Time</param>
        /// <param name="OnExpireAction"><see cref="Action">Action</see> to Run as a Task if the Loop Expires, not if you manually break the Loop</param>
        /// <returns>True if Loop should Continue; False if it should Stop</returns>
        public static async Task<bool> Delay(DateTime startTime, TimeSpan delayTimeSpan, TimeSpan expireTimeSpan, Action OnExpireAction)
        {
            // Wait Delay
            await Task.Delay(delayTimeSpan).ConfigureAwait(false);
            TimeSpan t = DateTime.UtcNow - startTime;

            // Expire
            if (t > expireTimeSpan)
            {
#if DEBUG
                Console.WriteLine("Expired: " + t);
#endif
                // Run Action as Task
                _ = Task.Run(() => OnExpireAction()).ConfigureAwait(false);

                // Return and Stop
                return false;
            }
#if DEBUG
            Console.WriteLine("Continue: " + t);
#endif
            // Return and Continue
            return true;
        }

        /// <summary>
        /// Loop and Delay - Return at the Expire Time
        /// <para>Returns bool indicating if the loop should continue (You can ignore this)</para>
        /// <para>There are 10,000 Ticks in a millisecond</para>
        /// <para>Runs an Action only when the Loop Expires, not if you manually break the Loop</para>
        /// <para>Simple, Faster</para>
        /// </summary>
        /// <param name="startTicks">When the loop started in "Ticks"</param>
        /// <param name="delayMs">Delay in Milliseconds</param>
        /// <param name="expireMs">How long to wait before the Loop indicates it should Stop</param>
        /// <param name="OnExpireAction"><see cref="Action">Action</see> to Run as a Task if the Loop Expires, not if you manually break the Loop</param>
        /// <returns>True if Loop should Continue; False if it should Stop</returns>
        public static async Task<bool> Delay(long startTicks, int delayMs, int expireMs, Action OnExpireAction)
        {
            // Wait Delay
            await Task.Delay(delayMs).ConfigureAwait(false);
            long t = DateTime.UtcNow.Ticks - startTicks;

            // Expire (avoids any infinite loop)
            if (t > (expireMs * 10000))
            {
                // Run Action as Task
                _ = Task.Run(() => OnExpireAction()).ConfigureAwait(false); 
#if DEBUG
                Console.WriteLine("Expired: " + t);
#endif
                // Return and Stop
                return false;
            }
#if DEBUG
            Console.WriteLine("Continue: " + t);
#endif
            // Return and Continue
            return true;
        }
    }
}