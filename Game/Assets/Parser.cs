using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parser : MonoBehaviour {
	

	private static string[] tokenize(string tkn)
	{
		string[] tokens;
		tokens = tkn.Split(default(string[]),System.StringSplitOptions.RemoveEmptyEntries);
		if (tokens.Length <= 0)
		{
			return new string[0];
		}
		return tokens;


	}

	public static string Parse (string input) 
	{
		string[] token = tokenize(input);
		if (token.Length <= 0)
		{
			return "Please enter a valid command";
		}
		return "valid";

	}
}
