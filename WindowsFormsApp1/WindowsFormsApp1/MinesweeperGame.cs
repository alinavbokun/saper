using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class MinesweeperGame : Game
    {
        private readonly int SIDEX, SIDEY;
        private GameObject[,] gameField;
        private int countMinesOnField;
        //private static readonly String MINE = "\uD83D\uDCA3";
        //private static readonly String FLAG = "\uD83D\uDEA9";
        private int countFlags;
        private bool isGameStopped;
        private int countClosedTiles;
        int difficulty;
        Form myForm;
        public int rest;
        GameObject clue;
        bool isClue;

        public void DeleteLabels()
        {
            for (int i = 0; i < SIDEX; ++i)
            {
                for (int j = 0; j < SIDEY; ++j)
                {
                    myForm.Controls.Remove(gameField[i,j]);
                }
            }
        }
        private void ShowAllField()
        {
            for (int i = 0; i < SIDEX; ++i)
            {
                for (int j = 0; j < SIDEY; ++j)
                {
                    GameObject obj = gameField[i, j];
                    if (obj.isMine == true)
                    {
                        if (obj.isFlag != true) //setCellColor(i, j, Color.Red, gameField);
                        {
                            //setCellColor(i, j, Color.Red, gameField);
                            //setCellValue(i, j, MINE, gameField);
                            setBomb(i, j, gameField);
                        }
                    }
                    else
                    {
                        setCellColor(i, j, Color.Green, gameField);
                        setCellNumber(i, j, obj.countMineNeighbors, gameField);
                        obj.Image = null;
                    }
                }
            }
        }

        public MinesweeperGame(Form form, int sidex, int sidey, int diff, bool isCl)
        {
            myForm = form;
            SIDEX = sidex;
            SIDEY = sidey;
            gameField = new GameObject[SIDEX, SIDEY];
            countClosedTiles = SIDEX * SIDEY;
            difficulty = diff;
            switch (SIDEX)
            {
                case 10: size = 1; break;
                case 20: size = 2; break;
                case 30: size = 3; break;
                default: size = 1; break;
            }
            clue = null;
            isClue = isCl;
        }

        public void restart()
        {
            countFlags = 0;
            countMinesOnField = 0;
            isGameStopped = false;
            countClosedTiles = SIDEX * SIDEY;
            clue = null;
            restartGame();
            if (isClue == true) openTile(clue.x, clue.y);
        }
        private void win()
        {
            isGameStopped = true;
            MessageBox.Show("WIN!!!");
        }
        private void gameOver()
        {
            isGameStopped = true;
            ShowAllField();
            MessageBox.Show("LOOOSER!!!");
        }
        private void markTile(int x, int y)
        {
            GameObject obj = gameField[x, y];
            if (isGameStopped == true) return;
            if (obj.isOpen == true || (countFlags == 0 && obj.isFlag == false)) return;
            else
            {
                if (obj.isFlag == false)
                {
                    obj.isFlag = true;
                    //setCellColor(x, y, Color.Yellow, gameField);
                    //setCellValue(x, y, FLAG, gameField);
                    setFlag(x, y, gameField);
                    countFlags--;
                    if (countClosedTiles == countMinesOnField && countFlags == 0) { win(); return; }
                }
                else
                {
                    obj.isFlag = false;
                    //setCellColor(x, y, Color.Orange, gameField);
                    //setCellValue(x, y, "", gameField);
                    obj.Image = null;
                    countFlags++;
                }
            }
        }
        private void openTile(int x, int y)
        {

            GameObject obj = gameField[x,y];
            if (obj.isOpen == true || obj.isFlag == true || isGameStopped == true) return;
            if (obj.isMine == true)
            {
                //obj.isOpen = true;
                //countClosedTiles--;
                //setCellColor(x, y, Color.Red, gameField);
                //setCellValue(x, y, MINE, gameField);
                gameOver();
                return;
            }
            else
            {
                if (obj.countMineNeighbors != 0)
                {
                    setCellNumber(x, y, obj.countMineNeighbors, gameField);
                    obj.isOpen = true;
                    countClosedTiles--;
                    setCellColor(x, y, Color.Green, gameField);
                    if (countClosedTiles == countMinesOnField && countFlags == 0) { win(); return; }
                    return;
                }
                obj.isOpen = true;
                countClosedTiles--;
                setCellColor(x, y, Color.Green, gameField);
                setCellValue(x, y, "", gameField);
                if (countClosedTiles == countMinesOnField && countFlags == 0) { win(); return; }
                List<GameObject> list = getNeighbors(obj);
                foreach (GameObject a in list)
                {
                    if (a.isOpen == false)
                        openTile(a.x, a.y);
                }
            }

        }
        private List<GameObject> getNeighbors(GameObject o)
        {
            List<GameObject> list = new List<GameObject>();
            int x = o.x;
            int y = o.y;
            for (int i = x - 1; i < x + 2; ++i)
            {
                for (int j = y - 1; j < y + 2; ++j)
                {
                    if (!(i < 0 || j < 0 || i > (SIDEX - 1) || j > (SIDEY - 1) || (i == x && j == y)))
                    {
                        list.Add(gameField[i,j]);
                    }
                }
            }
            return list;
        }
        private void countMineNeighbors()
        {
            
            for (int i = 0; i < SIDEX; ++i)
            {
                for (int j = 0; j < SIDEY; ++j)
                {
                    if (gameField[i,j].isMine == false)
                    {
                        List<GameObject> list = getNeighbors(gameField[i,j]);
                        foreach (GameObject a in list)
                        {
                            if (a.isMine == true) gameField[i,j].countMineNeighbors++;
                        }
                        if (clue == null && gameField[i, j].countMineNeighbors == 0) clue = gameField[i, j];
                    }
                }
            }
        }
        private void createGame()
        {
            bool isMine;
            int r;
            Random ran = new Random();
            countMinesOnField = 0;
            int dy = (myForm.Size.Height - 65)/SIDEY;
            int dx = dy;
            myForm.Width = dx * SIDEX + 20;
            for (int i = 0; i < SIDEX; ++i)
            {
                for (int j = 0; j < SIDEY; ++j)
                {
                    r = ran.Next(100/difficulty);
                    if (r == 0) { isMine = true; countMinesOnField++; }
                    else isMine = false;
                    gameField[i,j] = new GameObject(i, j, isMine);
                    gameField[i, j].BorderStyle = BorderStyle.Fixed3D;
                    gameField[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    gameField[i, j].Font = new Font("TimesNewRoman", 14, FontStyle.Regular);
                    gameField[i, j].Location = new Point(i * dx, j * dy + 26);
                    gameField[i, j].Height = dy;
                    gameField[i, j].Width = dx;
                    gameField[i, j].MouseClick += button1_MouseDown;
                    myForm.Controls.Add(gameField[i, j]);
                    setCellColor(i, j, Color.Orange, gameField);
                    setCellValue(i, j, "", gameField);
                }
            }
            countMineNeighbors();
            countFlags = countMinesOnField;
        }
        private void restartGame()
        {
            bool isMine;
            int r;
            Random ran = new Random();
            for (int i = 0; i < SIDEX; ++i)
            {
                for (int j = 0; j < SIDEY; ++j)
                {
                    gameField[i, j].isOpen = false;
                    gameField[i, j].isFlag = false;
                    gameField[i, j].countMineNeighbors = 0;
                    r = ran.Next(100/difficulty);
                    if (r == 0) { isMine = true; countMinesOnField++; }
                    else isMine = false;
                    gameField[i, j].isMine = isMine;
                    gameField[i, j].Image = null;
                    setCellColor(i, j, Color.Orange, gameField);
                    setCellValue(i, j, "", gameField);
                }
            }
            countMineNeighbors();
            countFlags = countMinesOnField;
        }

        public void initialize()
        {
            createGame();
            if (isClue == true) openTile(clue.x, clue.y);
        }
        public void onMouseLeftClick(int x, int y)
        {
            if (isGameStopped == true)
            {
                //restart();
                return;
            }
            openTile(x, y);
        }
        public void onMouseRightClick(int x, int y)
        {
            markTile(x, y);
        }
        public void button1_MouseDown(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                onMouseLeftClick(((GameObject) sender).x, ((GameObject)sender).y);
            }               
            if (e.Button == MouseButtons.Right)
            {
                onMouseRightClick(((GameObject)sender).x, ((GameObject)sender).y);
            }
        }
    }
}
