using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CrazySharp
{
	public partial class Form1 : Form
	{
		public Bitmap[] CardImages;
		public Field[] GameField;
		public int[] CardStack;

		public int GameTime;
		public int Points;
		public bool GameOver;

		public Random TheRandom;
		public Timer GameTimer;

		public Color Selected;
		public Color Unselected;

		public Form1()
		{
			InitializeComponent();

			this.CardImages = new Bitmap[81];
			for (int I1 = 0; I1 < 3; I1++)
			{
				for (int I2 = 0; I2 < 3; I2++)
				{
					for (int I3 = 0; I3 < 3; I3++)
					{
						for (int I4 = 0; I4 < 3; I4++)
						{
							int I = I1 + I2 * 3 + I3 * 9 + I4 * 27;
							this.CardImages[I] = CreateCard(I1, I2, I3, I4);
						}
					}
				}
			}

			this.CardStack = new int[81];

			this.GameField = new Field[16];
			for (int Y = 0; Y < 4; Y++)
			{
				for (int X = 0; X < 4; X++)
				{
					int I = X + Y * 4;

					this.GameField[I] = new Field();
					this.GameField[I].PictureBoxIndex = I;
					this.GameField[I].CardIndex = -1;
					this.GameField[I].IsSelected = false;

					this.GameField[I].Location = new Point(X * 160 + 10, Y * 160 + 50);
					this.GameField[I].Size = new Size(150, 150);
					this.GameField[I].BorderStyle = BorderStyle.FixedSingle;
					this.GameField[I].BackColor = this.Unselected;

					this.GameField[I].MouseClick += delegate(object sender, MouseEventArgs e)
					{
						if (e.Button != MouseButtons.Left) { return; }
						if (this.GameOver) { return; }
						if (this.GameField[I].CardIndex == -1) { return; }

						if (this.GameField[I].IsSelected)
						{
							this.GameField[I].IsSelected = false;
							this.GameField[I].BackColor = this.Unselected;
						}
						else
						{
							int SelectedCount = 0;
							for (int I2 = 0; I2 < 16; I2++)
							{
								if (this.GameField[I2].IsSelected) { SelectedCount++; }
							}

							if (SelectedCount < 3)
							{
								this.GameField[I].IsSelected = true;
								this.GameField[I].BackColor = this.Selected;
								SelectedCount++;
							}

							if (SelectedCount == 3)
							{
								int[] Selection = new int[3];
								int Index = 0;
								for (int I2 = 0; I2 < 16; I2++)
								{
									if (this.GameField[I2].IsSelected)
									{
										Selection[Index] = I2;
										Index++;
									}
								}

								GameState TheGameState = GameState(this.GameField[Selection[0]].CardIndex, this.GameField[Selection[1]].CardIndex, this.GameField[Selection[2]].CardIndex);
								if (TheGameState.Valid)
								{
									this.Points += TheGameState.Points;
									this.label_points.Text = "Points: " + this.Points;
									this.label_info.Text = "Valid combination.";

									for (int I2 = 0; I2 < 16; I2++)
									{
										if (this.GameField[I2].IsSelected)
										{
											this.GameField[I2].IsSelected = false;
											this.GameField[I2].BackColor = this.Unselected;
											this.GameField[I2].Image = null;
											this.GameField[I2].CardIndex = -1;

											for (int I3 = 0; I3 < 81; I3++)
											{
												if (this.CardStack[I3] == -1) { continue; }
												this.GameField[I2].Image = this.CardImages[CardStack[I3]];
												this.GameField[I2].CardIndex = this.CardStack[I3];
												this.CardStack[I3] = -1;
												break;
											}
										}
									}

									int StackCount = 0;
									for (int I2 = 0; I2 < 81; I2++)
									{
										if (this.CardStack[I2] != -1) { StackCount++; }
									}

									if (StackCount == 0)
									{
										int FieldCount = 0;
										for (int I2 = 0; I2 < 16; I2++)
										{
											if (this.GameField[I2].CardIndex != -1) { FieldCount++; }
										}

										if (FieldCount == 0)
										{
											this.label_info.Text = "The game is over.";
											this.label_combinations.Text = "Combinations: 0";
											this.GameOver = true;
											this.GameTimer.Stop();
											return;
										}
									}

									if (!this.GameOver) { this.label_combinations.Text = "Combinations: " + PossibleCombinations().Count; }
								}
								else
								{
									this.label_info.Text = "Not a valid combination.";
								}
							}
						}
					};

					this.Controls.Add(this.GameField[I]);
				}
			}

			this.GameTime = 0;
			this.Points = 0;
			this.GameOver = true;

			this.TheRandom = new Random();

			this.Selected = Color.FromArgb(191, 191, 191);
			this.Unselected = Color.FromArgb(255, 255, 255);

			this.GameTimer = new Timer();
			this.GameTimer.Interval = 1000;
			this.GameTimer.Tick += delegate(object sender, EventArgs e)
			{
				this.GameTime++;
				int Seconds = this.GameTime % 60;
				int Minutes = this.GameTime / 60 % 60;
				int Hours = this.GameTime / 60 / 60 % 24;
				this.label_time.Text = String.Format("Time: {0:d2}:{1:d2}:{2:d2}", Hours, Minutes, Seconds);
			};
			this.KeyDown += delegate(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Escape) { this.Close(); }
			};
			this.button_new.Click += delegate(object sender, EventArgs e)
			{
				this.label_info.Text = "Starting a new game.";
				this.label_time.Text = "Time: 00:00:00";
				this.label_points.Text = "Points: 0";

				for (int I = 0; I < 81; I++)
				{
					this.CardStack[I] = I;
				}

				for (int I = 80; I >= 0; I--)
				{
					int NewInt = this.TheRandom.Next(I + 1);
					int Temp = this.CardStack[I];
					this.CardStack[I] = this.CardStack[NewInt];
					this.CardStack[NewInt] = Temp;
				}

				for (int I = 0; I < 16; I++)
				{
					this.GameField[I].Image = this.CardImages[this.CardStack[I]];
					this.GameField[I].CardIndex = this.CardStack[I];
					this.GameField[I].IsSelected = false;
					this.GameField[I].BackColor = this.Unselected;
					this.CardStack[I] = -1;
				}

				this.GameTime = 0;
				this.Points = 0;
				this.GameOver = false;
				this.GameTimer.Start();

				this.label_combinations.Text = "Combinations: " + PossibleCombinations().Count;
			};
			this.button_mix.Click += delegate(object sender, EventArgs e)
			{
				if (this.GameOver) { return; }

				if (this.Points >= 10)
				{
					this.label_info.Text = "Mixing cards.";

					this.Points -= 10;
					this.label_points.Text = "Points: " + this.Points;

					for (int I1 = 0; I1 < 16; I1++)
					{
						if (this.GameField[I1].CardIndex == -1) { continue; }

						for (int I2 = 0; I2 < 81; I2++)
						{
							if (this.CardStack[I2] != -1) { continue; }
							this.CardStack[I2] = this.GameField[I1].CardIndex;
							this.GameField[I1].Image = null;
							this.GameField[I1].IsSelected = false;
							this.GameField[I1].BackColor = this.Unselected;
							this.GameField[I1].CardIndex = -1;
							break;
						}
					}

					for (int I = 80; I >= 0; I--)
					{
						int NewInt = this.TheRandom.Next(I + 1);
						int Temp = this.CardStack[I];
						this.CardStack[I] = this.CardStack[NewInt];
						this.CardStack[NewInt] = Temp;
					}

					for (int I1 = 0; I1 < 16; I1++)
					{
						for (int I2 = 0; I2 < 81; I2++)
						{
							if (this.CardStack[I2] == -1) { continue; }

							this.GameField[I1].Image = this.CardImages[this.CardStack[I2]];
							this.GameField[I1].CardIndex = this.CardStack[I2];
							this.GameField[I1].IsSelected = false;
							this.GameField[I1].BackColor = this.Unselected;
							this.CardStack[I2] = -1;
							break;
						}
					}

					this.label_combinations.Text = "Combinations: " + PossibleCombinations().Count;
				}
				else
				{
					this.label_info.Text = "At least 10 points to mix.";
				}
			};
		}

		public Bitmap CreateCard(int Shape, int Count, int TheColor, int Filling)
		{
			Bitmap CardBitmap = new Bitmap(150, 150);
			Graphics CardGraphics = Graphics.FromImage(CardBitmap);

			Bitmap ShapeBitmap = CreateShape(Shape, TheColor, Filling);

			if (Count == 0)
			{
				CardGraphics.DrawImage(ShapeBitmap, 50, 50);
			}
			else if (Count == 1)
			{
				CardGraphics.DrawImage(ShapeBitmap, 15, 50);
				CardGraphics.DrawImage(ShapeBitmap, 85, 50);
			}
			else if (Count == 2)
			{
				CardGraphics.DrawImage(ShapeBitmap, 50, 15);
				CardGraphics.DrawImage(ShapeBitmap, 15, 85);
				CardGraphics.DrawImage(ShapeBitmap, 85, 85);
			}

			return CardBitmap;
		}
		public Bitmap CreateShape(int Shape, int TheColor, int Filling)
		{
			Bitmap NewBitmap = new Bitmap(50, 50);
			Graphics TheGraphics = Graphics.FromImage(NewBitmap);
			TheGraphics.SmoothingMode = SmoothingMode.AntiAlias;

			Color[] ColorArrayOut = new Color[3];
			ColorArrayOut[0] = Color.FromArgb(255, 0, 0);
			ColorArrayOut[1] = Color.FromArgb(0, 255, 0);
			ColorArrayOut[2] = Color.FromArgb(0, 0, 255);

			Color[] ColorArrayIn = new Color[3];
			ColorArrayIn[0] = Color.FromArgb(255, 127, 127);
			ColorArrayIn[1] = Color.FromArgb(127, 255, 127);
			ColorArrayIn[2] = Color.FromArgb(127, 127, 255);

			HatchBrush NewHatchBrush = new HatchBrush(HatchStyle.DiagonalCross, ColorArrayOut[TheColor], this.Unselected);
			SolidBrush NewSolidBrush = new SolidBrush(ColorArrayIn[TheColor]);

			if (Shape == 0)
			{
				if (Filling == 0)
				{
					TheGraphics.DrawEllipse(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
				else if (Filling == 1)
				{
					TheGraphics.FillEllipse(NewHatchBrush, 1, 1, 46, 46);
					TheGraphics.DrawEllipse(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
				else if (Filling == 2)
				{
					TheGraphics.FillEllipse(NewSolidBrush, 1, 1, 46, 46);
					TheGraphics.DrawEllipse(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
			}
			else if (Shape == 1)
			{
				GraphicsPath NewGraphicsPath = new GraphicsPath();
				NewGraphicsPath.AddLine(1, 46, 24, 1);
				NewGraphicsPath.AddLine(24, 1, 46, 46);
				NewGraphicsPath.CloseFigure();

				if (Filling == 0)
				{
					TheGraphics.DrawPath(new Pen(ColorArrayOut[TheColor], 3), NewGraphicsPath);
				}
				else if (Filling == 1)
				{
					TheGraphics.FillPath(NewHatchBrush, NewGraphicsPath);
					TheGraphics.DrawPath(new Pen(ColorArrayOut[TheColor], 3), NewGraphicsPath);
				}
				else if (Filling == 2)
				{
					TheGraphics.FillPath(NewSolidBrush, NewGraphicsPath);
					TheGraphics.DrawPath(new Pen(ColorArrayOut[TheColor], 3), NewGraphicsPath);
				}
			}
			else if (Shape == 2)
			{
				if (Filling == 0)
				{
					TheGraphics.DrawRectangle(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
				else if (Filling == 1)
				{
					TheGraphics.FillRectangle(NewHatchBrush, 1, 1, 46, 46);
					TheGraphics.DrawRectangle(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
				else if (Filling == 2)
				{
					TheGraphics.FillRectangle(NewSolidBrush, 1, 1, 46, 46);
					TheGraphics.DrawRectangle(new Pen(ColorArrayOut[TheColor], 3), 1, 1, 46, 46);
				}
			}

			return NewBitmap;
		}
		public GameState GameState(int A, int B, int C)
		{
			if (A > B) { (A, B) = (B, A); }
			if (B > C) { (B, C) = (C, B); }

			int A1 = A % 3;
			int A2 = B % 3;
			int A3 = C % 3;

			int B1 = A / 3 % 3;
			int B2 = B / 3 % 3;
			int B3 = C / 3 % 3;

			int C1 = A / 9 % 3;
			int C2 = B / 9 % 3;
			int C3 = C / 9 % 3;

			int D1 = A / 27 % 3;
			int D2 = B / 27 % 3;
			int D3 = C / 27 % 3;

			bool A4 = A1 == A2 && A2 == A3;
			bool B4 = B1 == B2 && B2 == B3;
			bool C4 = C1 == C2 && C2 == C3;
			bool D4 = D1 == D2 && D2 == D3;

			bool A5 = A1 != A2 && A1 != A3 && A2 != A3;
			bool B5 = B1 != B2 && B1 != B3 && B2 != B3;
			bool C5 = C1 != C2 && C1 != C3 && C2 != C3;
			bool D5 = D1 != D2 && D1 != D3 && D2 != D3;

			if (A4 && B4 && C4 && D5) { return new GameState(true, 1); }
			if (A4 && B4 && C5 && D4) { return new GameState(true, 1); }
			if (A4 && B4 && C5 && D5) { return new GameState(true, 2); }
			if (A4 && B5 && C4 && D4) { return new GameState(true, 1); }
			if (A4 && B5 && C4 && D5) { return new GameState(true, 2); }
			if (A4 && B5 && C5 && D4) { return new GameState(true, 2); }
			if (A4 && B5 && C5 && D5) { return new GameState(true, 3); }
			if (A5 && B5 && C5 && D5) { return new GameState(true, 4); }
			if (A5 && B5 && C5 && D4) { return new GameState(true, 3); }
			if (A5 && B5 && C4 && D5) { return new GameState(true, 3); }
			if (A5 && B5 && C4 && D4) { return new GameState(true, 2); }
			if (A5 && B4 && C5 && D5) { return new GameState(true, 3); }
			if (A5 && B4 && C5 && D4) { return new GameState(true, 2); }
			if (A5 && B4 && C4 && D5) { return new GameState(true, 2); }
			if (A5 && B4 && C4 && D4) { return new GameState(true, 1); }

			return new GameState(false, 0);
		}
		public List<int[]> PossibleCombinations()
		{
			List<int[]> Combinations = new List<int[]>();
			List<int> Buffer = new List<int>();

			for (int I1 = 0; I1 < 16; I1++)
			{
				if (this.GameField[I1].CardIndex == -1) { continue; }

				for (int I2 = 0; I2 < 16; I2++)
				{
					if (this.GameField[I2].CardIndex == -1) { continue; }
					if (I1 == I2) { continue; }

					for (int I3 = 0; I3 < 16; I3++)
					{
						if (this.GameField[I3].CardIndex == -1) { continue; }
						if (I1 == I3) { continue; }
						if (I2 == I3) { continue; }

						if (Buffer.Contains(this.GameField[I1].CardIndex + this.GameField[I2].CardIndex + this.GameField[I3].CardIndex)) { continue; }

						if (GameState(this.GameField[I1].CardIndex, this.GameField[I2].CardIndex, this.GameField[I3].CardIndex).Valid)
						{
							Combinations.Add(new int[] { I1, I2, I3 });
							Buffer.Add(this.GameField[I1].CardIndex + this.GameField[I2].CardIndex + this.GameField[I3].CardIndex);
						}
					}
				}
			}

			return Combinations;
		}
	}
	public class Field : PictureBox
	{
		public int PictureBoxIndex;
		public int CardIndex;
		public bool IsSelected;
	}
	public class GameState
	{
		public bool Valid;
		public int Points;

		public GameState(bool Valid, int Points)
		{
			this.Valid = Valid;
			this.Points = Points;
		}
	}
}
