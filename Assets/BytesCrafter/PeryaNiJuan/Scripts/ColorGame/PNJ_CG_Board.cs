using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HungryCannibal.UnderTheSeaUIKit.ProgressBars;

public class PNJ_CG_Board : MonoBehaviour 
{
	public GameObject infoBoard;
	public CounterBar cashDisplay;
	public List<Text> colorCalcs;
	public Text totalBet;
	public Text totalWin;
	public string billSymbol = "B";

	public void Show()
	{
		if( CustomReference.Access.gameState == PNJ_GameState.DropBall)
		{
			for(var i = 0; i < colorCalcs.Count; i++)
			{
				int[] bets = CustomReference.Access.dropBall.colorPicker.colorPicked;
				PNJ_PP_ResultBall result = CustomReference.Access.dropBall.GetListWin();
				int currentFrequency = 0;
				switch (i)
				{
					case 0:
						currentFrequency = result.xThreeRed ? CustomReference.Access.dropBall.xThreeRed : 0;
						break;
					case 1:
						currentFrequency = result.xOneStar ? CustomReference.Access.dropBall.xOneStar : 0;
						break;
					case 2:
						currentFrequency = result.xThreeWhite ? CustomReference.Access.dropBall.xThreeWhite : 0;
						break;
					case 3:
						currentFrequency = result.xTwoRed ? CustomReference.Access.dropBall.xTwoRed : 0;
						break;
					case 4:
						currentFrequency = result.xTwoStar ? CustomReference.Access.dropBall.xTwoStar : 0;
						break;
					case 5:
						currentFrequency = result.xTwoWhite ? CustomReference.Access.dropBall.xTwoWhite : 0;
						break;
					default:
						currentFrequency = 0;
						break;
				}
				int totalCalc = bets[i] * currentFrequency;
				colorCalcs[i].text = billSymbol + bets[i] + " x " + billSymbol + currentFrequency + " = "+ billSymbol + totalCalc;

				totalBet.text = billSymbol + CustomReference.Access.dropBall.colorPicker.GetTotalBet;
				totalWin.text = billSymbol + CustomReference.Access.resultInfo.resultItem.totalToReceived;
			}
		}
		
		infoBoard.SetActive(true);
	}

	public void Hide()
	{
		infoBoard.SetActive(false);
	}

	public void UpdateCashDisplay(int cash, int increment)
	{
		cashDisplay.count = cash;
		cashDisplay.IncrementCount(increment, false);
	}

	public void Reset()
	{
		for (int index = 0; index < colorCalcs.Count; index++) 
		{
			colorCalcs[index].text = "Waiting...";
		}

		totalBet.text = "None";
		totalWin.text = "None";
	}

	public void Bet(PNJ_CG_Result preResult)
	{
		
		for(var index = 0; index < preResult.colorBets.Count; index++)
		{
			colorCalcs[index].text = billSymbol + " " + preResult.colorBets[index].getBetsTotal;
		}
		totalBet.text = billSymbol + " " + preResult.getTotalPlayBet;
	}

	public void Set(PNJ_CG_Result result)
	{
		for (int index = 0; index < colorCalcs.Count; index++) 
		{
			colorCalcs[index].text = billSymbol + " " + result.colorBets [index].getBetsTotal + " X " + result.result [index] 
				+ " = " + billSymbol + " " + (result.colorBets [index].getBetsTotal * result.result [index]);
		}

		totalBet.text = billSymbol + " " + result.getTotalPlayBet;
		totalWin.text = billSymbol + " " +  result.getTotalPlayWin;
	}
}