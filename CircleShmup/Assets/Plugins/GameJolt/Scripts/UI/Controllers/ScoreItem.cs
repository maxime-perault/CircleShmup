﻿using UnityEngine;
using UnityEngine.UI;

namespace GameJolt.UI.Controllers
{
	public class ScoreItem : MonoBehaviour
	{
		public Text username;
		public Text value;

		public Color defaultColour = Color.white;
		public Color highlightColour = Color.green;

		public void Init(API.Objects.Score score)
		{
			username.text = score.PlayerName;
			value.text = score.Text;

			bool isUserScore = score.UserID != 0 && API.Manager.Instance.CurrentUser != null && API.Manager.Instance.CurrentUser.ID == score.UserID;
			username.color = isUserScore ? highlightColour : defaultColour;
			value.color = isUserScore ? highlightColour : defaultColour;
		}
	}
}