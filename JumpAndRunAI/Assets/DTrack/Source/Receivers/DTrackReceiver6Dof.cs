/* Unity DTrack Plugin: DTrackReceiver6Dof.cs
 *
 * Script providing DTRACK Standard Body data to a GameObject.
 *
 * Copyright (c) 2020-2022 Advanced Realtime Tracking GmbH & Co. KG
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

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DTrackSDK;
using DTrack;
using DTrack.Util;


namespace DTrack
{


public class DTrackReceiver6Dof : DTrackReceiver
{
	float deadzone = 150.0f;
	float startheight = 0.0f;
	CapsuleCollider CapsuleCollider;
		Transform playerCamera;
					public		Vector3 DrackPos;
		AutoScroll levlelänge;
	bool iscrouching = false;
		public bool boss = false;

	PlayerMovement JumpCrouch;

	[ Tooltip( "Standard Body ID as seen in DTRACK" ) ]
	public int bodyId;

	[ Tooltip( "Update position of this GameObject" ) ]
	public bool applyPosition = true;
	[ Tooltip( "Update rotation of this GameObject" ) ]
	public bool applyRotation = true;


	public void Calibrate(DTrackSDK.DTrackBody dtBody)
    {

		startheight = dtBody.Loc[2];
			
	}

	void OnEnable()
	{
			levlelänge = GameObject.Find("GameManager").GetComponent<AutoScroll>();
		
		JumpCrouch = GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>();
		CapsuleCollider = GameObject.Find("PlayerObject").GetComponent<CapsuleCollider>();
			playerCamera = GameObject.Find("PlayerCamera").transform;
		this.Register();
	}

     void OnDisable()
	{
		this.Unregister();
	}


	void Update()
	{
		DTrackSDK.Frame frame = GetDTrackFrame();  // ensures data integrity against DTrack class
		if ( frame == null )  return;  // no new tracking data
		if ( frame.Bodies == null )  return;

		try
		{
			DTrackSDK.DTrackBody dtBody;

			if ( frame.Bodies.TryGetValue( this.bodyId - 1, out dtBody ) )
			{
				if ( dtBody.IsTracked )
				{
					if (this.applyPosition)
					{
                            // from Drack dtBody.Loc[1] = x dtBody.Loc[0] = y dtBody.Loc[2] = z
                            if (!boss)
                            {

						 DrackPos = new Vector3(this.transform.position.x, this.transform.position.y, -dtBody.Loc[1] * 0.002f);
                            }
                            if (boss)
                            {
								 DrackPos = new Vector3(dtBody.Loc[0] * 0.005f-(levlelänge.LevelLänge+levlelänge.startRow+13/2), this.transform.position.y, -dtBody.Loc[1] * 0.005f);
							}
							


								this.transform.position = DrackPos;
								if (startheight == 0.0f)
								{
									Calibrate(dtBody);

									Debug.Log(startheight);
								}

								if (dtBody.Loc[2] >= startheight + deadzone)
								{
									Debug.Log("springen");
									JumpCrouch.Jump6Dof();
								}
								if (dtBody.Loc[2] <= startheight - deadzone)
								{

									CapsuleCollider.height = 0.975f;
									CapsuleCollider.center = new Vector3(CapsuleCollider.center.x, -0.5f, CapsuleCollider.center.z);
									playerCamera.position = new Vector3(playerCamera.position.x, 7502.0f, playerCamera.position.z);
                                if (!iscrouching)
                                {
									FindObjectOfType<AudioManager>().Stop("walk");
									FindObjectOfType<AudioManager>().Play("slidebegin");
									FindObjectOfType<AudioManager>().PlayDelayed("slideloop", "slidebegin");
								}
									iscrouching = true;

								}

								if (iscrouching && dtBody.Loc[2] >= startheight - deadzone * 2)
								{
									CapsuleCollider.center = new Vector3(CapsuleCollider.center.x, 0.0f, CapsuleCollider.center.z);
									CapsuleCollider.height = 1.95f;
									playerCamera.position = new Vector3(playerCamera.position.x, 7503.0f, playerCamera.position.z);
									FindObjectOfType<AudioManager>().Stop("slideloop");
									FindObjectOfType<AudioManager>().Play("slideend");
								iscrouching = false;
								}
							

                    }

					if (this.applyRotation)
					{

						//Matrix4x4 DrackRotMatrix = new Matrix4x4(new Vector4(1.0f, 0.0f, 0.0f, 0.0f), new Vector4(0.0f, 0.0f, 0.0f, 0.0f), new Vector4(1.0f, 0.0f, 0.0f, 0.0f), new Vector4(1.0f, 0.0f, 0.0f, 0.0f))
						Quaternion DrackRot = ConvertRotation.ToUnity(dtBody.Quaternion);
						Vector3 vecrot = DrackRot.eulerAngles;

							

						this.transform.rotation = Quaternion.Euler(0.0f,  vecrot.y, 0.0f);
						//this.transform.Rotate(new Vector3(0.0f, -180, 0), Space.World);
                    }
                }
			}
		}
		catch ( Exception e )
		{
			Debug.Log( $"Error while moving object: {e}" );
		}
	}
}


}  // namespace DTrack

