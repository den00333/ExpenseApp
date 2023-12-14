using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    internal class offlineFunc
    {
        offWalletUC o;
        public offlineFunc(offWalletUC ow)
        {
            this.o = ow;
        }
        public static void createTXT(String path)
        {
            string fileContent =
                "Wallets|Balance|Amount:0" + Environment.NewLine +
                "Wallets|Expense|Amount:0" + Environment.NewLine +
                "Wallets|Expense|Short:0" + Environment.NewLine +
                "Wallets|LogWallet|LogWallet!" + Environment.NewLine +
                "Expenses:";

            // Write the content to the text file
            File.WriteAllText(path, fileContent);

        }

        public static void updateOnWallet(int loc, String path, double finalTotal)
        {
            //1 ->balance   2->expense
            String[] lines = File.ReadAllLines(path);
            String[] splittedLines = lines[loc].Split(':');
            lines[loc] = lines[loc].Replace(splittedLines[1], finalTotal.ToString());
            File.WriteAllLines(path, lines);

        }
        public bool minus(String path, double val)
        {

            String[] lines = File.ReadAllLines(path);
            String[] line0 = lines[0].Split(':');
            String[] line1 = lines[1].Split(':');
            double balance = double.Parse(line0[1]);
            double expense = double.Parse(line1[1]);
            double total1 = expense - val;
            Console.WriteLine("total1= " + total1);
            if (total1 < 0)
            {
                DialogResult res = MessageBox.Show("Do you want to use your savings for this expense?", "Insufficient Amount", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    double total2 = balance + total1;
                    Console.WriteLine($"{total2} = {balance}  - {total1}");
                    if (total2 < 0)
                    {
                        MessageBox.Show("You don't have enough balance.", "Insufficient Amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    else
                    {

                        updateOnWallet(0, path, total2);
                        updateOnWallet(1, path, 0);
                        double shortAmount = double.Parse(readTxt(path, 2)) + total1;

                        o.lblExpenses.Text = otherFunc.amountBeautify(float.Parse("0"));
                        o.lblBalance.Text = otherFunc.amountBeautify((float)total2);
                        if (shortAmount >= 0)
                        {
                            updateOnWallet(2, path, 0);
                            o.lblExpenses.Text = otherFunc.amountBeautify(0);
                            o.lblShort.Hide();
                        }
                        else
                        {
                            updateOnWallet(2, path, shortAmount);
                            o.lblShort.Text = otherFunc.amountBeautify((float)shortAmount);
                            o.lblShort.Show();
                            o.lblShort.ForeColor = Color.Red;
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                updateOnWallet(1, path, total1);

                o.lblExpenses.Text = otherFunc.amountBeautify((float)total1);
                return true;
            }
        }



        public static String readTxt(String path, int loc)
        {
            if (File.Exists(path))
            {
                String[] lines = File.ReadAllLines(path);
                String[] splitted = lines[loc].Split(':');
                return splitted[1];
            }
            else
            {
                createTXT(path);
                String[] lines = File.ReadAllLines(path);
                String[] splitted = lines[loc].Split(':');
                return splitted[1];
            }
        }

    }
}