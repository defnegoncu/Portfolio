/* Unity DTrack Plugin: Transform.cs
 *
 * Helper routines to convert from ART to Unity world.
 *
 * Copyright (c) 2019-2022 Advanced Realtime Tracking GmbH & Co. KG
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. Neither the name of copyright holder nor the names of its contributors
 *    may be used to endorse or promote products derived from this software
 *    without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using UnityEngine;

namespace DTrack.Util
{


public class ConvertPosition
{
	private static readonly float MM_TO_M = 0.001f;

	// Convert ART position vector to Unity position vector.

	public static Vector3 ToUnity( float x, float y, float z )
	{
		// swap axes as DTRACK uses right-handed world space, convert unit to 'meter'
		return new Vector3( x * MM_TO_M, z * MM_TO_M, y * MM_TO_M );
	}


	// Convert ART position vector to Unity position vector.
	//   p = [ x, y, z ]

	public static Vector3 ToUnity( float[] p )
	{
		// swap axes as DTRACK uses right-handed world space, convert unit to 'meter'
		return new Vector3( p[ 0 ] * MM_TO_M, p[ 2 ] * MM_TO_M, p[ 1 ] * MM_TO_M );
	}
}


public class ConvertRotation
{
	// Convert ART rotation quaternion to Unity rotation quaternion.
	//   q = [ w, x, y, z ]

	public static Quaternion ToUnity( float[] q )
	{
		// swap axes and direction as DTRACK uses right-handed world space
		return new Quaternion( q[ 1 ], q[ 3 ], q[ 2 ], -q[ 0 ] );
	}
}


}  // namespace DTrack.Util

