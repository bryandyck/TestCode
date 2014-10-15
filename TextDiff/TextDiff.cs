/* Version:         1.0 (ported from version 2.4 original file)                *
* Date:             11 june 2004                                               *
* Compilers:        C#  (Microsoft Visual Studio .NET 2003)                    *
* Ported to C# by:  Andrew A. Fedorov (support@bizkit-soft.com)                *
* Autor: 	    Angus Johnson - angusj-AT-myrealbox-DOT-com					   *
* Copyright:        © 2001-2004 Angus Johnson                                  *
*                                                                              *
* Licence to use, terms and conditions:                                        *
*           The code in the TextDiff component is released as freeware 	       *
*           provided you agree to the following terms & conditions:    	       *
*           1. the copyright notice, terms and conditions are          	       *
*           left unchanged                                             	       *
*           2. modifications to the code by other authors must be      	       *
*           clearly documented and accompanied by the modifier's name. 	       *
*           3. the TextDiff component may be freely compiled into binary       *
*           format and no acknowledgement is required. However, a      	       *
*           discrete acknowledgement would be appreciated (eg. in a    	       *
*           program's 'About Box').                                    	       *
*                                                                              *
* Description:      Component to list differences between two integer arrays   *
*                   using a "longest common subsequence" algorithm.            *
*                   Typically, this component is used to diff 2 text files     *
*                   once their individuals lines have been hashed.             *
*                                                                              *
* Acknowledgements: The key algorithm in this component is based on:           *
*                   "An O(ND) Difference Algorithm and its Variations"         *
*                   By E Myers - Algorithmica Vol. 1 No. 2, 1986, pp. 251-266  *
*                   http://www.cs.arizona.edu/people/gene/                     *
*                   http://www.cs.arizona.edu/people/gene/PAPERS/diff.ps       */
using System;
using System.Collections;
using System.Text;
using BizKit.Checksums;
using System.Windows.Forms;

namespace BizKit.TextDiff
{
	public enum ScriptKind {None, AddRange, DelRange, AddDel};
	public enum ChangeKind {None, Add, Delete, Modify};

	public class ChangeRec
	{
		public ChangeKind Kind; //(ckAdd, ckDelete, ckckModify)
		public int x;			//Array1 offset (where to add, delete, modify)
		public int y;			//Array2 offset (what to add, modify)
		public int Range;
	}

	public class THashList : IEnumerable
	{
		private ArrayList _List = null;
		private Crc32 crc32;
		public THashList()
		{
			_List = new ArrayList();
			crc32 = new Crc32();
		}

		/// <summary>
		/// Gets the number of elements actually contained in the list.
		/// </summary>
		public int Count 
		{
			get 
			{ 
				return _List.Count; 
			}
		}

		private long StrToCrc(string value)
		{
			crc32.Reset();
			for (int i = 0; i < value.Length; i++)
			{
				crc32.Update(Convert.ToInt16(value[i]));
			}
			return crc32.Value;
		}

		public long[] ToLong()
		{
			Type type = typeof(long[]);
			return (long[])_List.ToArray(type);
		}

		public void FromStrArray(string[] value)
		{
			this.Clear();
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add(string value)
		{
			return _List.Add(StrToCrc(value));
		}

		public long this[int index]
		{
			get
			{
				return (long)_List[index];
			}
		}

		/// <summary>
		/// Removes all elements from the list.
		/// </summary>
		public void Clear()
		{
			_List.Clear();
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the list.
		/// </summary>
		/// <returns>An IEnumerator for the entire list.</returns>
		public IEnumerator GetEnumerator()
		{
			return _List.GetEnumerator();
		}
	}
        
	public class TChangeList : IEnumerable
	{
		private ArrayList _List = null;
		public TChangeList()
		{
			_List = new ArrayList();
		}

		/// <summary>
		/// Gets the number of elements actually contained in the list.
		/// </summary>
		public int Count 
		{
			get 
			{ 
				return _List.Count; 
			}
		}

		/// <summary>
		/// Adds an permission object to the end of the list.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add(ChangeRec value)
		{
			return _List.Add(value);
		}

		/// <summary>
		/// Specifies the allocated size of the TList object.
		/// </summary>
		public int Capacity
		{
			get
			{
				return _List.Count;
			}
			set
			{
				_List.Clear();
				for (int i = 0; i <= value; i++)
				{
					_List.Add(null);
				}
			}
		}

		public ChangeRec this[int index]
		{
			get
			{
				return (ChangeRec)_List[index];
			}
			set
			{
				_List[index] = value;
			}
		}

		/// <summary>
		/// Removes all elements from the list.
		/// </summary>
		public void Clear()
		{
			_List.Clear();
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the list.
		/// </summary>
		/// <returns>An IEnumerator for the entire list.</returns>
		public IEnumerator GetEnumerator()
		{
			return _List.GetEnumerator();
		}
	}

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class TextDiffer
	{
		//Maximum realistic deviation from centre diagonal vector ...
		private const int MAX_DIAGONAL = 0xFFFFFF;
		
		private class DiagVectorArray //= new int[2*MAX_DIAGONAL];
		{
			/// <summary>
			/// Convert negative array index to positive one.
			/// </summary>
			/// <param name="value">Array index.</param>
			/// <returns></returns>
			private int NegIndex(int value)
			{
				return (_size + value);
			}

			private int _size;
			int[] _array;
			/// <summary>
			/// 
			/// </summary>
			/// <param name="value">array size.</param>
			public DiagVectorArray(int value)
			{
				_size = value;
				_array = new int[value*2 + 1];
			}

			public int this[int index]
			{
				get
				{
					return _array[NegIndex(index)];
				}	
				set
				{
					int ind = NegIndex(index);
					_array[ind] = value;
				}
			}
		}

		int MaxD;
		TChangeList _ChangeList = null;
		ChangeRec fLastAdd, fLastDel, fLastMod;
		DiagVectorArray diagVecB = null; //forward  array  = new int[2*MAX_DIAGONAL]
		DiagVectorArray diagVecF = null; //backward array = new int[2*MAX_DIAGONAL]
		long[] Array1, Array2;
		bool fCancelled;

		public TextDiffer()
		{
			_ChangeList = new TChangeList();
		}

		public TChangeList ChangeList
		{
			get
			{
				return _ChangeList;
			}
		}

		private byte[] StrToByte(string value)
		{
			byte[] k = new byte[value.Length];
			for(int i = 0; i < value.Length; i++)
			{
				k[i] = Convert.ToByte(value[i]);
			}
			return k;
		}

		private long[] StrToLong(string value)
		{
			long[] k = new long[value.Length];
			for(int i = 0; i < value.Length; i++)
			{
				k[i] = Convert.ToInt64(value[i]);
			}
			return k;
		}

		public bool Execute(string Arr1, string Arr2)
		{
			return this.Execute(StrToLong(Arr1), StrToLong(Arr2));
		}

		public bool Execute(long[] Arr1, long[] Arr2)
		{
			bool result = false;
			ClearChanges();

			if ((Arr1 == null) || (Arr2 == null))
				return result;
			Array1 = Arr1;
			Array2 = Arr2;

			//MaxD == Maximum possible deviation from centre diagonal vector
			//which can't be more than the largest intArray (with upperlimit = MAX_DIAGONAL) ...
			MaxD = Math.Min(Math.Max(Arr1.Length, Arr2.Length), MAX_DIAGONAL);

			//estimate the no. changes == 1/8 total size rounded to a 32bit boundary
			//_ChangeList.Capacity = ((int)Math.Max(MaxD, 1024) / 32)*4;

			diagVecF = new DiagVectorArray(MaxD);
			diagVecB = new DiagVectorArray(MaxD);
 
			int bottom1 = 0;
			int bottom2 = 0;
			int top1 = Arr1.Length - 1;
			int top2 = Arr2.Length - 1;

			//ignore leading and trailing matches, we're only interested in diffs...
			//20-Jan-04: this code block was moved from RecursiveDiff()
			while ((bottom1 <= top1) && (bottom2 <= top2) && (Array1[bottom1] == Array2[bottom2]))
			{
				bottom1++; bottom2++;
			}
			while ((top1 > bottom1) && (top2 > bottom2) && (Array1[top1] == Array2[top2]))
			{
				top1--; top2--;
			}

			//NOW DO IT HERE...
			if ((bottom1 > top1) && (bottom2 > top2)) //24-Jan-04
			{
				result = true; //ie identical arrays
			}
			else
			{
				result = RecursiveDiff(bottom1/* -1*/, bottom2/* -1*/, top1 + 1, top2 + 1);
			}

			//add remaining range buffers onto ChangeList...
			PushAdd(); PushDel();
			if (!result)
				_ChangeList.Clear();

			return result;
		}

		private bool Odd(int value)
		{
			return (value % 2) != 0;
		}

		private bool RecursiveDiff(int bottom1, int bottom2, int top1, int top2)
		{
			bool result = true;

			//check if just all additions or all deletions...
			if (top1 == bottom1) 
			{
				AddToScript(bottom1, bottom2, top1, top2, ScriptKind.AddRange);
				return result;
			}
			else
			{
				if (top2 == bottom2) 
				{
					AddToScript(bottom1, bottom2, top1, top2, ScriptKind.DelRange);
					return result;
				}
			}

			int curr1;
			int curr2;
			//Delta = offset of bottomright from topleft corner
			int Delta = (top1 - bottom1) - (top2 - bottom2);
			//initialize the forward and backward diagonal vectors including the
			//outer bounds ...
			diagVecF[0] = bottom1;
			diagVecB[Delta] = top1;
			//24-Jan-04 ...
			diagVecF[top1 - bottom1] = top1;
			diagVecB[top1 - bottom1] = top1;
			//the following avoids the -(top2 - bottom2) vectors being assigned
			//invalid values. Also, the algorithm is a little faster than when
			//initialising the -(top2 - bottom2) vectors instead...
			if (Delta > 0)
			{
				diagVecF[-(top2 - bottom2 +1)] = bottom1;
				diagVecB[-(top2 - bottom2 +1)] = bottom1;
			}

			//nb: the 2 arrays of diagonal vectors diagVecF and diagVecB store the
			//current furthest forward and backward Array1 coords respectively
			//(something between bottom1 and top1). The Array2 coords can be derived
			//from these (see added comments (1) at the bottom of this unit).
		
			//When the forward and backward arrays cross over at some point the
			//curr1 and curr2 values represent a relative mid-point on the 'shortest
			//common sub-sequence' path. By recursively finding these points the
			//whole path can be constructed.

			//OUTER LOOP ...
			//MAKE INCREASING OSCILLATIONS ABOUT CENTRE DIAGONAL UNTIL A FORWARD
			//DIAGONAL VECTOR IS GREATER THAN OR EQUAL TO A BACKWARD DIAGONAL.
			for (int D = 1; D <= MaxD; D++)
			{
				Application.DoEvents();
				if (fCancelled)
				{
					return false;
				}

				//forward loop...............................................
				//nb: k == index of current diagonal vector and
				//    will oscillate (in increasing swings) between -MaxD and MaxD
				int k = -D;
				//stop going outside the grid ...
				while (k < -(top2 - bottom2))
					k += 2;

				int i = Math.Min(D, top1 - bottom1 -1); //24-Jan-04
				while (k <= i)
				{
					//derive curr1 from the larger of adjacent vectors...
					if ((k == -D) || ((k < D) && (diagVecF[k-1] < diagVecF[k+1]))) 
					{
						curr1 = diagVecF[k+1];
					}
					else
					{
						curr1 = diagVecF[k-1]+1;
					}
					//derive curr2 (see above) ...
					curr2 = curr1 - bottom1 + bottom2 - k; //andrew
					//while (curr1+1,curr2+1) match, increment them...
					while ((curr1 < top1) && (curr2 < top2) && (Array1[curr1/*+1*/] == Array2[curr2/*+1*/]))
					{
						curr1++; curr2++;
					}
					//update current vector ...
					diagVecF[k] = curr1;

					//check if a vector in diagVecF overlaps the corresp. diagVecB vector.
					//(If a crossover point found here then further recursion is required.)
					if (Odd(Delta) && (k > -D + Delta) && (k < D + Delta) && (diagVecF[k] >= diagVecB[k]))
					{
						//find subsequent points by recursion ...

						//To avoid declaring 2 extra variables in this recursive function ..
						//Delta & k are simply reused to store the curr1 & curr2 values ...
						Delta = curr1; k = curr2;
						//ignore trailing matches in lower block ...
						while ((curr1 > bottom1) && (curr2 > bottom2) && (Array1[curr1-1] == Array2[curr2-1]))
						{
							curr1--; curr2--;
						}
						result = RecursiveDiff(bottom1,bottom2,curr1,curr2);
						//do recursion with the upper block...
						if (!result)
							return result;
						//and again with the lower block (nb: Delta & k are stored curr1 & curr2)...
						result = RecursiveDiff(Delta,k,top1,top2);
						return result; //All done!!!
					}
					k += 2;
				}
			

				//backward loop..............................................
				//nb: k will oscillate (in increasing swings) between -MaxD and MaxD
				k = -D + Delta;
				//stop going outside grid and also ensure we remain within the diagVecB[]
				//and diagVecF[] array bounds.
				//nb: in the backward loop it is necessary to test the bottom left corner.
				while (k < -(top2 - bottom2))
				{
					k += 2;
				}
				i = Math.Min(D + Delta, top1 - bottom1 - 1); //24-Jan-04
				while (k <= i)
				{
					//derive curr1 from the adjacent vectors...
					if ((k == D + Delta) || ((k > -D + Delta) && (diagVecB[k + 1] > diagVecB[k - 1])))
					{
						curr1 = diagVecB[k - 1];
					}
					else
					{
						curr1 = diagVecB[k + 1] - 1;
					}
					curr2 = curr1 - bottom1 + bottom2 - k; //andrews
					//if curr2 < bottom2 then break; //shouldn't be necessary and adds a 3% time penalty

					//slide up and left if possible ...
					while ((curr1 > bottom1) && (curr2 > bottom2) && (Array1[curr1-1] == Array2[curr2-1])) 
					{
						curr1--; curr2--;
					}
					//update current vector ...
					diagVecB[k] = curr1;

					//check if a crossover point reached...
					if (!Odd(Delta) && (k >= -D) && (k <= D) && (diagVecF[k] >= diagVecB[k]))
					{
						if ((bottom1+1 == top1) && (bottom2+1 == top2))
						{
							//ie smallest divisible unit
							//(nb: skAddDel could also have been skDelAdd)
							AddToScript(bottom1, bottom2, top1, top2, ScriptKind.AddDel);
						} 
						else
						{
							//otherwise process the lower block ...
							result = RecursiveDiff(bottom1, bottom2, curr1, curr2);
							if (!result)
								return result;
							//strip leading matches in upper block ...
							while ((curr1 < top1) && (curr2 < top2) && (Array1[curr1/*+1*/] == Array2[curr2/*+1*/]))
							{
								curr1++; curr2++;
							}
							//and finally process the upper block ...
							result = RecursiveDiff(curr1, curr2, top1, top2);
						}
						return result; //All done!!!
					}
					k += 2;
				}
			}
			return false;
		}

		private void PushAdd()
		{
			PushMod();
			if (fLastAdd != null) 
				_ChangeList.Add(fLastAdd);
			fLastAdd = null;
		}

		private void PushDel()
		{
			PushMod();
			if (fLastDel != null) 
				_ChangeList.Add(fLastDel);
			fLastDel = null;
		}

		private void PushMod()
		{
			if (fLastMod != null) 
				_ChangeList.Add(fLastMod);
			fLastMod = null;
		}

		private void TrashAdd()
		{
			fLastAdd = null;
		}

		private void TrashDel()
		{
			fLastDel = null;
		}

		private void NewAdd(int bottom1, int bottom2)
		{
			fLastAdd = new ChangeRec();
			fLastAdd.Kind = ChangeKind.Add;
			fLastAdd.x = bottom1;
			fLastAdd.y = bottom2;
			fLastAdd.Range = 1;
		}

		private void NewMod(int bottom1, int bottom2)
		{
			fLastMod = new ChangeRec();
			fLastMod.Kind = ChangeKind.Modify;
			fLastMod.x = bottom1;
			fLastMod.y = bottom2;
			fLastMod.Range = 1;
		}

		private void NewDel(int bottom1)
		{
			fLastDel = new ChangeRec();
			fLastDel.Kind = ChangeKind.Delete;
			fLastDel.x = bottom1;
			fLastDel.y = 0;
			fLastDel.Range = 1;
		}

		// 1. there can NEVER be concurrent fLastAdd and fLastDel record ranges.
		// 2. fLastMod is always pushed onto ChangeList before fLastAdd & fLastDel.
		private void Add(int bottom1, int bottom2)
		{
			if (fLastAdd != null)                  //OTHER ADDS PENDING
			{
				if ((fLastAdd.x == bottom1) && ((fLastAdd.y + fLastAdd.Range) == bottom2))
					fLastAdd.Range++;                     //add in series
				else 
				{
					PushAdd(); NewAdd(bottom1, bottom2);    //add NOT in series
				}
			}
			else
			{
				if (fLastDel != null)              //NO ADDS BUT DELETES PENDING
				{
					if (bottom1 == fLastDel.x)                   //add matches pending del so modify ...
					{
						if ((fLastMod != null) && 
							((fLastMod.x + fLastMod.Range - 1) == bottom1) &&
							((fLastMod.y + fLastMod.Range - 1) == bottom2)) 
						{
							fLastMod.Range++;                   //modify in series
						}
						else 
						{
							PushMod(); NewMod(bottom1, bottom2);  //start NEW modify
						}
						if (fLastDel.Range == 1)
							TrashDel();     //decrement or remove existing del
						else 
						{
							fLastDel.Range--; fLastDel.x++;
						}
					}
					else 
					{
						PushDel(); NewAdd(bottom1, bottom2);    //add does NOT match pending del's
					}
				}
				else
					NewAdd(bottom1,bottom2);                            //NO ADDS OR DELETES PENDING
			}
		}

		private void Delete(int bottom1)
		{
			if (fLastDel != null)                   //OTHER DELS PENDING
			{
				if ((fLastDel.x + fLastDel.Range) == bottom1)
					fLastDel.Range++;                     //del in series
				else 
				{
					PushDel(); NewDel(bottom1);       //del NOT in series
				}
			}
			else
			{
				if (fLastAdd != null)              //NO DELS BUT ADDS PENDING
				{
					if (bottom1 == fLastAdd.x)                   //del matches pending add so modify ...
					{
						if ((fLastMod != null) && ((fLastMod.x + fLastMod.Range) == bottom1))
						{
							fLastMod.Range++;                           //mod in series
						}
						else 
						{
							PushMod(); NewMod(bottom1,fLastAdd.y); //start NEW modify ...
						}
						if (fLastAdd.Range == 1) 
						{
							TrashAdd();     //decrement or remove existing add
						}
						else 
						{
							fLastAdd.Range--; 
							fLastAdd.x++; 
							fLastAdd.y++;
						}
					}
					else 
					{
						PushAdd(); 
						NewDel(bottom1);       //del does NOT match pending add's
					}
				}
				else
				{
					NewDel(bottom1);                               //NO ADDS OR DELETES PENDING
				}
			}
		}

		//This is a bit UGLY but simply reduces many adds & deletes to many fewer
		//add, delete & modify ranges which are then stored in ChangeList...
		private void AddToScript(int bottom1, int bottom2, int top1, int top2, ScriptKind ScriptKind)
		{
			switch (ScriptKind)
			{
				case ScriptKind.AddRange:
					for (int i = bottom2; i <= top2 - 1; i++)
					{
						Add(bottom1, i);
					}
					break;
				case ScriptKind.DelRange:
					for (int i = bottom1; i <= top1 - 1; i++)
					{
						Delete(i);
					}
					break;
				case ScriptKind.AddDel:
					Add(bottom1, bottom2); 
					Delete(top1-1);
					break;
			}
		}

		public void Cancel()
		{
			fCancelled = true;
		}

		public void ClearChanges()
		{
			_ChangeList.Clear();
		}

		public int ChangeCount
		{
			get
			{
				return _ChangeList.Count;
			}
		}

		public ChangeRec this[int index]
		{
			get
			{
				return _ChangeList[index];
			}
		}
	}
}
