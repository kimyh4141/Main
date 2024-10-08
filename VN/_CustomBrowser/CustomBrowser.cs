using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using WiseM.Data;
using WiseM.Forms;
using System.IO;
using System.Data.SqlClient;
using WiseM.Browser.Rework;

namespace WiseM.Browser
{
    public class BrowserUserService : BrowserCustomService
    {
        public override bool IsBrowserCustomLogin()
        {
            return true;
        }

        public override void BrowserCustomLogin(WiseM.AppService.LoginController controller)
        {
            new CustomLogin(controller).ShowDialog();
        }

        protected override void FilterRow(FilterRowEventArgs e)
        {
            int count = e.Values.Count;
            switch (e.Program.ToLower())
            {
                case "prd013":
                    for (int i = 0; i < count; i++)
                    {
                        switch (e.Values.Keys[i])
                        {
                            case "T08" when Convert.ToDouble(e.Values["T08"].Value) / Convert.ToDouble(e.Values["Avg"].Value) * 100 <= 90:
                                e.Values["T08"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T08"].BackColor = Color.Red;
                                break;
                            case "T08" when (Convert.ToDouble(e.Values["T08"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T08"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T08"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T08"].BackColor = Color.Orange;
                                break;
                            case "T08":
                            {
                                if ((Convert.ToDouble(e.Values["T08"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T08"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T08"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T09" when (Convert.ToDouble(e.Values["T09"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T09"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T09"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T09" when (Convert.ToDouble(e.Values["T09"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T09"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T09"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T09"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T09":
                            {
                                if ((Convert.ToDouble(e.Values["T09"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T09"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T09"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T10" when (Convert.ToDouble(e.Values["T10"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T10"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T10"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T10" when (Convert.ToDouble(e.Values["T10"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T10"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T10"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T10"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T10":
                            {
                                if ((Convert.ToDouble(e.Values["T10"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T10"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T10"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T11" when (Convert.ToDouble(e.Values["T11"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T11"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T11"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T11" when (Convert.ToDouble(e.Values["T11"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T11"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T11"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T11"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T11":
                            {
                                if ((Convert.ToDouble(e.Values["T11"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T11"].ApplyColor = WeFilterColor.BackColor;
                                    e.Values["T11"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T12" when (Convert.ToDouble(e.Values["T12"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T12"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T12"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T12" when (Convert.ToDouble(e.Values["T12"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T12"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T12"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T12"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T12":
                            {
                                if ((Convert.ToDouble(e.Values["T12"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T12"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T12"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T13" when (Convert.ToDouble(e.Values["T13"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T13"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T13"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T13" when (Convert.ToDouble(e.Values["T13"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T13"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T13"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T13"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T13":
                            {
                                if ((Convert.ToDouble(e.Values["T13"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T13"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T13"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T14" when (Convert.ToDouble(e.Values["T14"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T14"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T14"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T14" when (Convert.ToDouble(e.Values["T14"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T14"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T14"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T14"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T14":
                            {
                                if ((Convert.ToDouble(e.Values["T14"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T14"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T14"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T15" when (Convert.ToDouble(e.Values["T15"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T15"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T15"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T15" when (Convert.ToDouble(e.Values["T15"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T15"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T15"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T15"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T15":
                            {
                                if ((Convert.ToDouble(e.Values["T15"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T15"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T15"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T16" when (Convert.ToDouble(e.Values["T16"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T16"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T16"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T16" when (Convert.ToDouble(e.Values["T16"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T16"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T16"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T16"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T16":
                            {
                                if ((Convert.ToDouble(e.Values["T16"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T16"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T16"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T17" when (Convert.ToDouble(e.Values["T17"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T17"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T17"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T17" when (Convert.ToDouble(e.Values["T17"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T17"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T17"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T17"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T17":
                            {
                                if ((Convert.ToDouble(e.Values["T17"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T17"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T17"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T18" when (Convert.ToDouble(e.Values["T18"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T18"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T18"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T18" when (Convert.ToDouble(e.Values["T18"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T18"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T18"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T18"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T18":
                            {
                                if ((Convert.ToDouble(e.Values["T18"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T18"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T18"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T19" when (Convert.ToDouble(e.Values["T19"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T19"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T19"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T19" when (Convert.ToDouble(e.Values["T19"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T19"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T19"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T19"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T19":
                            {
                                if ((Convert.ToDouble(e.Values["T19"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T19"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T19"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T20" when (Convert.ToDouble(e.Values["T20"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T20"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T20"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T20" when (Convert.ToDouble(e.Values["T20"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T20"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T20"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T20"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T20":
                            {
                                if ((Convert.ToDouble(e.Values["T20"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T20"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T20"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T21" when (Convert.ToDouble(e.Values["T21"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T21"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T21"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T21" when (Convert.ToDouble(e.Values["T21"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T21"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T21"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T21"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T21":
                            {
                                if ((Convert.ToDouble(e.Values["T21"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T21"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T21"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T22" when (Convert.ToDouble(e.Values["T22"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T22"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T22"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T22" when (Convert.ToDouble(e.Values["T22"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T22"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T22"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T22"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T22":
                            {
                                if ((Convert.ToDouble(e.Values["T22"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T22"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T22"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T23" when (Convert.ToDouble(e.Values["T23"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T23"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T23"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T23" when (Convert.ToDouble(e.Values["T23"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T23"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T23"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T23"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T23":
                            {
                                if ((Convert.ToDouble(e.Values["T23"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T23"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T23"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T00" when (Convert.ToDouble(e.Values["T00"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T00"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T00"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T00" when (Convert.ToDouble(e.Values["T00"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T00"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T00"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T00"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T00":
                            {
                                if ((Convert.ToDouble(e.Values["T00"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T00"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T00"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T01" when (Convert.ToDouble(e.Values["T01"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T01"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T01"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T01" when (Convert.ToDouble(e.Values["T01"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T01"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T01"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T01"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T01":
                            {
                                if ((Convert.ToDouble(e.Values["T01"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T01"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T01"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T02" when (Convert.ToDouble(e.Values["T02"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T02"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T02"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T02" when (Convert.ToDouble(e.Values["T02"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T02"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T02"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T02"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T02":
                            {
                                if ((Convert.ToDouble(e.Values["T02"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T02"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T02"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T03" when (Convert.ToDouble(e.Values["T03"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T03"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T03"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T03" when (Convert.ToDouble(e.Values["T03"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T03"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T03"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T03"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T03":
                            {
                                if ((Convert.ToDouble(e.Values["T03"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T03"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T03"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T04" when (Convert.ToDouble(e.Values["T04"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T04"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T04"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T04" when (Convert.ToDouble(e.Values["T04"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T04"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T04"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T04"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T04":
                            {
                                if ((Convert.ToDouble(e.Values["T04"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T04"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T04"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T05" when (Convert.ToDouble(e.Values["T05"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T05"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T05"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T05" when (Convert.ToDouble(e.Values["T05"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T05"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T05"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T05"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T05":
                            {
                                if ((Convert.ToDouble(e.Values["T05"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T05"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T05"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T06" when (Convert.ToDouble(e.Values["T06"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T06"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T06"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T06" when (Convert.ToDouble(e.Values["T06"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T06"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T06"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T06"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T06":
                            {
                                if ((Convert.ToDouble(e.Values["T06"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T06"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T06"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                            case "T07" when (Convert.ToDouble(e.Values["T07"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 90:
                                e.Values["T07"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T07"].BackColor = System.Drawing.Color.Red;
                                break;
                            case "T07" when (Convert.ToDouble(e.Values["T07"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 90 && (Convert.ToDouble(e.Values["T07"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 <= 95:
                                e.Values["T07"].ApplyColor = WeFilterColor.BackColor;
                                e.Values["T07"].BackColor = System.Drawing.Color.Orange;
                                break;
                            case "T07":
                            {
                                if ((Convert.ToDouble(e.Values["T07"].Value) / Convert.ToDouble(e.Values["Avg"].Value)) * 100 > 95)
                                {
                                    e.Values["T07"].ApplyColor = WeFilterColor.BackColor;

                                    e.Values["T07"].BackColor = System.Drawing.Color.Green;
                                }

                                break;
                            }
                        }
                    }

                    break;
                case "qis1001":
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (e.Values.Keys[i] != "Routing") continue;
                        if (e.Values["Routing"].Value.ToString() != "SubTotal") continue;
                        e.Values["Line"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["Routing"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["BadName"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["BadQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["OutQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["OrderQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["RepairQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["RepairLossQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["ResultPPM"].ApplyColor = WeFilterColor.ForeColor;

                        e.Values["Line"].ForeColor = System.Drawing.Color.Red;
                        e.Values["Routing"].ForeColor = System.Drawing.Color.Red;
                        e.Values["BadName"].ForeColor = System.Drawing.Color.Red;
                        e.Values["BadQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["OutQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["OrderQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["RepairQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["RepairLossQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["ResultPPM"].ForeColor = System.Drawing.Color.Red;
                    }

                    break;
                }
                case "qis1002":
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (e.Values.Keys[i] != "BadName") continue;
                        if (e.Values["BadName"].Value.ToString() != "SubTotal") continue;
                        e.Values["Material"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["MaterialName"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["BadName"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["BadQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["OutQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["OrderQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["RepairQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["RepairLossQty"].ApplyColor = WeFilterColor.ForeColor;
                        e.Values["ResultPPM"].ApplyColor = WeFilterColor.ForeColor;

                        e.Values["Material"].ForeColor = System.Drawing.Color.Red;
                        e.Values["MaterialName"].ForeColor = System.Drawing.Color.Red;
                        e.Values["BadName"].ForeColor = System.Drawing.Color.Red;
                        e.Values["BadQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["OutQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["OrderQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["RepairQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["RepairLossQty"].ForeColor = System.Drawing.Color.Red;
                        e.Values["ResultPPM"].ForeColor = System.Drawing.Color.Red;
                    }

                    break;
                }

                case "wms011_1": //WMS011 
                    for (int i = 0; i < count; i++)
                    {
                        if (Convert.ToDouble(e.Values["Aging_4"].Value) != 0)
                        {
                            e.Values["Aging_4"].ApplyColor = WeFilterColor.BackColor;
                            e.Values["Aging_4"].BackColor = Color.Green;
                        }
                        if (Convert.ToDouble(e.Values["Aging_5"].Value) != 0)
                        {
                            e.Values["Aging_5"].ApplyColor = WeFilterColor.BackColor;
                            e.Values["Aging_5"].BackColor = Color.Orange;
                        }
                        if (Convert.ToDouble(e.Values["Aging_6"].Value) != 0)
                        {
                            e.Values["Aging_6"].ApplyColor = WeFilterColor.BackColor;
                            e.Values["Aging_6"].BackColor = Color.Red;
                        }
                    }

                    break;
            }

            // e.Cancel = true; // row 을 무시할때
        }

        public override SkinForm GetCustomProgramLinkForm(CustomProgramLinkEventArgs e)
        {
            switch (e.Program.ToLower())
            {
                case "erp001":
                    WorkOrder startWO = new WorkOrder();
                    return startWO;
                case "spc101":
                    SPCDataManagement.SPCDataManagement startSPCDataManagement
                        = new SPCDataManagement.SPCDataManagement();
                    return startSPCDataManagement;
                case "jig002":
                    JigSearch JigSearch = new JigSearch();
                    return JigSearch;
                case "jig102":
                    JigRepairsearch JigRepairsearch = new JigRepairsearch();
                    return JigRepairsearch;
                case "jig202":
                    JigWarehouseSearch JigWarehouseSearch = new JigWarehouseSearch();
                    return JigWarehouseSearch;
                case "dci001":
                    Dell_Cab_RawMaterialCheck Dell_Cab_RawMaterialCheck = new Dell_Cab_RawMaterialCheck();
                    return Dell_Cab_RawMaterialCheck;
                case "cst208":
                    Checksheet.CsCheckSheetSpec checkSheet = new Checksheet.CsCheckSheetSpec();
                    return checkSheet;
                case "prd802":
                    Outsourcing_Receipt_frmMain00 outsourcing_Receipt = new Outsourcing_Receipt_frmMain00();
                    return outsourcing_Receipt;
                case "prd803":
                    Outsourcing_FinishedGoods_frmMain00 outsourcing_FinishedGoods
                        = new Outsourcing_FinishedGoods_frmMain00();
                    return outsourcing_FinishedGoods;
                case "prd804":
                    ProcessPassing_frmMain00 ProcessPassing = new ProcessPassing_frmMain00();
                    return ProcessPassing;
            }

            return null;
        }

        public override void CustomPanelLinkMethod(CustomPanelLinkEventArgs e)
        {
            switch (e.Program.ToLower())
            {
                case "pn006":
                    switch (e.Link.ToLower())
                    {
                        case "shippingprocessing":
                            var Shipping_Process = new ShippingProcessing();
                            Shipping_Process.Show();
                            break;
                        case "movingprocessing":
                          
                            var MovingProcessing = new MovingProcessing();
                            MovingProcessing.Show();
                            break;
                    }

                    break;

                case "prm005":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm005_EditForm = new WiseMEdit.EditForm("Supply", e);
                        prm005_EditForm.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm106":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm106Process = new WiseMEdit.EditForm("Bom", e);
                        prm106Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "prm888":
                    var dataGridViewRow = e.DataGridView.CurrentRow;
                    switch (e.Panel)
                    {
                        case "Panel0":
                            switch (e.Link)
                            {
                                case "Delete":
                                    if (dataGridViewRow == null) return;
                                    if (DialogResult.Yes
                                        == System.Windows.Forms.MessageBox.Show
                                            (
                                             $"Are you sure?"
                                           , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                                            ))
                                    {
                                        var query = new StringBuilder();

                                        query.AppendLine
                                            (
                                             $@"
                                                DELETE
                                                  FROM FeederInfo
                                                 WHERE 1 = 1
                                                   AND Material = '{dataGridViewRow.Cells["Material"].Value}'
                                                   AND WorkCenter = '{dataGridViewRow.Cells["WorkCenter"].Value}'
                                            "
                                            );
                                        try
                                        {
                                            DbAccess.Default.ExecuteQuery(query.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show
                                                (
                                                 $"Deleted Error.\r\n{ex.Message}", "Error"
                                               , MessageBoxIcon.Error
                                                );
                                        }
                                        finally
                                        {
                                            e.AfterRefresh = WeRefreshPanel.Current;
                                            MessageBox.Show
                                                (
                                                 "Deleted Completed.", "Information"
                                               , MessageBoxIcon.Information
                                                );
                                        }
                                    }

                                    break;
                                case "ChangeFeeder":
                                    var feederInfoChangeFeeder = new FeederInfoChangeFeeder
                                        (
                                         dataGridViewRow.Cells["WorkCenter"].Value.ToString()
                                       , dataGridViewRow.Cells["Material"].Value.ToString()
                                        );
                                    feederInfoChangeFeeder.Show();
                                    feederInfoChangeFeeder.FormClosing += (sender, args) => { e.AfterRefresh = WeRefreshPanel.Current; };
                                    break;
                                case "ExcelUpload":
                                    var feederInfoUpload = new FeederInfoUpload();
                                    feederInfoUpload.Show();
                                    feederInfoUpload.FormClosing += (sender, args) => { e.AfterRefresh = WeRefreshPanel.Current; };
                                    break;
                            }

                            break;
                        case "Panel1":
                            switch (e.Link)
                            {
                                case "Delete":
                                    if (dataGridViewRow == null) return;
                                    if (DialogResult.Yes
                                        == System.Windows.Forms.MessageBox.Show
                                            (
                                             $"Are you sure?"
                                           , "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                                            ))
                                    {
                                        var query = new StringBuilder();

                                        query.AppendLine
                                            (
                                             $@"
                                            DELETE
                                              FROM FeederInfo
                                             WHERE 1 = 1
                                               AND Material = '{dataGridViewRow.Cells["Material"].Value}'
                                               AND WorkCenter = '{dataGridViewRow.Cells["WorkCenter"].Value}'
                                               AND Feeder = '{dataGridViewRow.Cells["Feeder"].Value}'
                                               AND RawMaterial = '{dataGridViewRow.Cells["RawMaterial"].Value}'
                                            ;
                                            "
                                            );
                                        try
                                        {
                                            DbAccess.Default.ExecuteQuery(query.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show
                                                (
                                                 $"Deleted Error.\r\n{ex.Message}", "Error"
                                               , MessageBoxIcon.Error
                                                );
                                        }
                                        finally
                                        {
                                            e.AfterRefresh = WeRefreshPanel.Current;
                                            MessageBox.Show
                                                (
                                                 "Deleted Completed.", "Information"
                                               , MessageBoxIcon.Information
                                                );
                                        }
                                    }

                                    break;
                            }

                            break;
                    }

                    break;

                case "wom100":
                    if (e.Link.ToLower() == "aa")
                    {
                        //MES.NewBarcode2 nb22 = new MES.NewBarcode2(e);
                        //nb22.ShowDialog();
                        BarcodeHist bch = new BarcodeHist();
                        bch.ShowDialog();
                    }

                    break;
                case "jig0011":
                    if (e.Link.ToLower() == "new")
                    {
                        JigBasisManagement JigBasisManagement = new JigBasisManagement();
                        JigBasisManagement.ShowDialog();
                        break;
                    }

                    if (e.Link.ToLower() == "update")
                    {
                        JigBasisManagementUpdate JigBasisManagement = new JigBasisManagementUpdate(e.DataGridView.SelectedCells[0].OwningRow.Cells["Jig"].Value.ToString());
                        JigBasisManagement.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                        break;
                    }

                    if (e.Link.ToLower() == "delete")
                    {
                        if (WiseApp.CurrentUserLevel.Name.Equals("Developer") == true
                            || WiseApp.CurrentUserLevel.Name.Equals("Manager") == true)
                        {
                            string currentJig = e.DataGridView.SelectedCells[0].OwningRow.Cells["Jig"].Value.ToString();
                            string messageStr = "선택한 Jig 데이터를 삭제합니다. Jig Information = " + currentJig + "' ";
                            if (DialogResult.Yes
                                == WiseM.MessageBox.Show
                                    (
                                     messageStr, "Delete", MessageBoxButtons.YesNo
                                   , MessageBoxIcon.Question
                                    ))
                            {
                                DataTable dt
                                    = DbAccess.Default.GetDataTable("Select * From JigMaintHist where Jig = '" + currentJig + "' ");
                                if (dt.Rows.Count > 0)
                                {
                                    WiseM.MessageBox.Show
                                        (
                                         "입출고, 보수 이력이 존재 함으로 삭제 할 수 없습니다.", "Information"
                                       , MessageBoxIcon.None
                                        );
                                    return;
                                }

                                try
                                {
                                    string DeleteQuery = "Delete  From Jig where Jig = '" + currentJig + "' ";
                                    string DeleteQuery1 = "Delete  From JigInfo where Jig = '" + currentJig + "' ";
                                    DbAccess.Default.ExecuteQuery(DeleteQuery);
                                    DbAccess.Default.ExecuteQuery(DeleteQuery1);
                                    WiseM.MessageBox.Show("데이터 삭제가 완료되었습니다.", "OK", MessageBoxIcon.None);
                                    e.AfterRefresh = WeRefreshPanel.Current;
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("해당 기능은 관리자 이외에 다른사람이 이용할 수 없습니다.", "Information", MessageBoxIcon.None);
                            //return;
                        }

                        break;
                    }

                    break;
                case "jig004":
                    switch (e.Link.ToLower())
                    {
                        case "new":
                        {
                            JigBasisManagement JigBasisManagement = new JigBasisManagement();
                            JigBasisManagement.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "edit":
                        {
                            JigBasisManagementUpdate JigBasisManagement
                                = new JigBasisManagementUpdate(e.DataGridView.CurrentRow.Cells["Jig"].Value.ToString());
                            JigBasisManagement.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete" when WiseApp.Id == "HQ0007":
                        {
                            string currentJig = e.DataGridView.CurrentRow.Cells["Jig"].Value as string;
                            string messageStr = "선택한 Jig 데이터를 삭제합니다. Jig Information = " + currentJig + "' ";
                            if (DialogResult.Yes
                                == WiseM.MessageBox.Show
                                    (
                                     messageStr, "Delete", MessageBoxButtons.YesNo
                                   , MessageBoxIcon.Question
                                    ))
                            {
                                DataTable dt = e.DbAccess.GetDataTable($"Select * From JigMaintHist where Jig = '{currentJig}' ");
                                if (dt.Rows.Count > 0)
                                {
                                    WiseM.MessageBox.Show
                                        (
                                         "입출고, 보수 이력이 존재 함으로 삭제 할 수 없습니다.", "Information"
                                       , MessageBoxIcon.None
                                        );
                                    return;
                                }

                                try
                                {
                                    string DeleteQuery = $"Delete  From Jig where Jig = '{currentJig}' ";
                                    string DeleteQuery1 = $"Delete  From JigInfo where Jig = '{currentJig}' ";
                                    e.DbAccess.ExecuteQuery(DeleteQuery);
                                    e.DbAccess.ExecuteQuery(DeleteQuery1);
                                    WiseM.MessageBox.Show("데이터 삭제가 완료되었습니다.", "OK", MessageBoxIcon.None);
                                    e.AfterRefresh = WeRefreshPanel.Current;
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            break;
                        }
                        case "delete":
                            WiseM.MessageBox.Show
                                (
                                 "해당 기능은 관리자 이외에 다른사람이 이용할 수 없습니다.", "Information"
                               , MessageBoxIcon.None
                                );
                            return;
                    }

                    break;
                case "jig102":
                    switch (e.Link.ToLower())
                    {
                        case "startmaint":
                        {
                            for (int i = 0; i < e.DataGridView.Rows.Count; i++)
                            {
                                string JigCode = string.Empty;
                                if (Convert.ToBoolean(e.DataGridView.Rows[i].Cells["Check"].Value.ToString()) != true) continue;
                                DataTable dt = e.DbAccess.GetDataTable($"Select * From JigMaintHist where Jig = N'{e.DataGridView.Rows[i].Cells["Jig3"].Value}' and ConfirmDate is null ");
                                if (dt.Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    JigCode = e.DataGridView.Rows[i].Cells["Jig3"].Value.ToString();
                                    RepairStart RepairStart = new RepairStart();
                                    RepairStart.ProcessStart(JigCode);
                                }
                            }

                            MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "writemaintinfo":
                        {
                            string JigCode = string.Empty;
                            string JigMaintHist = string.Empty;
                            JigCode = e.DataGridView.CurrentRow.Cells["Jig3"].Value.ToString();
                            JigMaintHist = e.DataGridView.CurrentRow.Cells["JigMaintHist1"].Value.ToString();
                            JigRepairInsert JigRepairInsert = new JigRepairInsert(JigCode, JigMaintHist);
                            JigRepairInsert.ShowDialog();
                            break;
                        }
                        case "managerconfirm":
                        {
                            if (WiseApp.Id == "shlee"
                                || WiseApp.Id == "gadi74"
                                || WiseApp.Id == "hykim"
                                || WiseApp.Id == "ybkim")
                            {
                                for (int i = 0; i < e.DataGridView.Rows.Count; i++)
                                {
                                    string JigCode = string.Empty;
                                    string JigMaintHist = string.Empty;
                                    if (Convert.ToBoolean(e.DataGridView.Rows[i].Cells["Check"].Value.ToString())) continue;
                                    JigCode = e.DataGridView.Rows[i].Cells["Jig3"].Value.ToString();
                                    JigMaintHist = e.DataGridView.Rows[i].Cells["JigMaintHist1"].Value.ToString();
                                    if (string.IsNullOrEmpty(JigMaintHist))
                                    {
                                    }
                                    else
                                    {
                                        JigConfirmProcess JigConfirmProcess = new JigConfirmProcess();
                                        JigConfirmProcess.ProcessStart(JigCode, JigMaintHist);
                                    }
                                }

                                MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                            }
                            else
                            {
                                MessageBox.Show
                                    (
                                     "Error! You don't have enough permissions.", "Error"
                                   , MessageBoxIcon.Error
                                    );
                                return;
                            }

                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                case "jig203":
                    switch (e.Link.ToLower())
                    {
                        case "stockin":
                        {
                            JigWareHouseIn JigWareHouseIn = new JigWareHouseIn();
                            JigWareHouseIn.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "stockout":
                        {
                            JigstockOut JigStockOut = new JigstockOut();
                            JigStockOut.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                case "jig103":
                    // if (e.Link.ToLower() == "write")
                    // {
                    //     JigRepairInsert jig103_Write = new JigRepairInsert(e);
                    //     jig103_Write.ShowDialog();
                    //     e.AfterRefresh = WeRefreshPanel.Current;
                    //     return;
                    // }

                    if (e.Link.ToLower() == "start")
                    {
                        int[] selectCells = e.DataGridView.SelectedCells.Cast<DataGridViewCell>()
                                             .Select(cell => cell.RowIndex).Distinct().ToArray();

                        foreach (var selectCell in selectCells)
                        {
                            string JigCode = string.Empty;

                            DataTable dt = DbAccess.Default.GetDataTable($"Select * From JigMaintHist where Jig = '{e.DataGridView.Rows[selectCell].Cells["Jig"].Value}' and ConfirmDate is null ");
                            if (dt.Rows.Count > 0) continue;
                            JigCode = e.DataGridView.Rows[selectCell].Cells["Jig"].Value.ToString();
                            var RepairStart = new RepairStart();
                            RepairStart.ProcessStart(JigCode);
                        }

                        e.AfterRefresh = WeRefreshPanel.Current;
                        break;
                    }

                    if (e.Link.ToLower() == "confirm")
                    {
                        if (WiseApp.CurrentUserLevel.Name.Equals("Developer") == false
                            && WiseApp.CurrentUserLevel.Name.Equals("Manager") == false)
                        {
                            MessageBox.Show("Error! You don't have enough user level.", "Error", MessageBoxIcon.Error);
                            return;
                        }

                        int[] selectedCells = e.DataGridView.SelectedCells.Cast<DataGridViewCell>()
                                               .Select(cell => cell.RowIndex).Distinct().ToArray();

                        foreach (var index in selectedCells)
                        {
                            string JigCode = string.Empty;
                            string JigMaintHist = string.Empty;
                            try
                            {
                                JigCode = e.DataGridView.Rows[index].Cells["Jig"].Value.ToString();
                                JigMaintHist = e.DataGridView.Rows[index].Cells["JigMaintHist"].Value.ToString();
                                if (string.IsNullOrEmpty(JigMaintHist) == false)
                                {
                                    JigConfirmProcess JigConfirmProcess = new JigConfirmProcess();
                                    JigConfirmProcess.ProcessStart(JigCode, JigMaintHist);
                                }
                            }
                            catch
                            {
                                MessageBox.Show
                                    (
                                     JigCode + " : It is a jig that does not start maintenance.", "Warining"
                                   , MessageBoxIcon.None
                                    );
                            }
                        }

                        MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                        e.AfterRefresh = WeRefreshPanel.Current;

                        break;
                    }

                    break;
                case "pn008":
                    if (e.Link.ToLower() == "to csv")
                    {
                        try
                        {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Elentec_INDIA_OCV_RawData_" + DateTime.Now.ToString("yyyMMdd") + "0000_" + e.DataGridView.Rows[0].Cells["MODELNAME"].Value.ToString() + ".CSV";
                            StreamWriter sw = new StreamWriter
                                (
                                 path,
                                 true, System.Text.Encoding.Default
                                );
                            ToCSV.WriteToStream(sw, (e.DataGridView.DataSource as DataTable), true, false);

                            if (MessageBox.ShowCaption
                                    (
                                     "Open the file?", "", MessageBoxButtons.YesNo
                                   , MessageBoxIcon.Question, null
                                    )
                                == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(path);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Stop, null);
                        }
                    }

                    break;
                case "prm801":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm801_EditForm = new WiseMEdit.EditForm("LineVoltage", e);
                        prm801_EditForm.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "prm802":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm802_EditForm = new WiseMEdit.EditForm("LineVoltage", e);
                        prm802_EditForm.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "prm900":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm FCTSpec = new EditForm(e);
                        FCTSpec.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                        //WiseMEdit.EditForm FCTSpec = new WiseMEdit.EditForm();
                        //FCTSpec.ShowDialog();
                        //e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "wip603":
                    if (e.Link.ToLower() == "export.csv")
                    {
                        try
                        {
                            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);

                            DateTimeFormatInfo inf = new DateTimeFormatInfo();
                            inf.LongTimePattern = "HH:mm:ss";
                            inf.ShortDatePattern = "yyyy-MM-dd";
                            info.DateTimeFormat = inf;

                            Thread.CurrentThread.CurrentCulture = info;

                            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + e.Program + " " + DateTime.Now.ToString("yyyMMddHHmmss") + ".CSV";
                            StreamWriter sw = new StreamWriter
                                (
                                 path,
                                 true, System.Text.Encoding.Default
                                );
                            ToCSV.WriteToStream(sw, (e.DataGridView.DataSource as DataTable), true, false);

                            if (MessageBox.ShowCaption
                                    (
                                     "Open the file?", "", MessageBoxButtons.YesNo
                                   , MessageBoxIcon.Question, null
                                    )
                                == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(path);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Stop, null);
                        }
                    }

                    break;
                case "wip699":
                    if (e.Link.ToLower() == "export.csv")
                    {
                        try
                        {
                            CultureInfo info = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);

                            DateTimeFormatInfo inf = new DateTimeFormatInfo();
                            inf.LongTimePattern = "HH:mm:ss";
                            inf.ShortDatePattern = "yyyy-MM-dd";
                            info.DateTimeFormat = inf;

                            Thread.CurrentThread.CurrentCulture = info;

                            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + e.Program + " " + DateTime.Now.ToString("yyyMMddHHmmss") + ".CSV";
                            StreamWriter sw = new StreamWriter
                                (
                                 path,
                                 true, System.Text.Encoding.Default
                                );
                            ToCSV.WriteToStream(sw, (e.DataGridView.DataSource as DataTable), true, false);

                            if (MessageBox.ShowCaption
                                    (
                                     "Open the file?", "", MessageBoxButtons.YesNo
                                   , MessageBoxIcon.Question, null
                                    )
                                == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(path);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Stop, null);
                        }
                    }

                    break;

                #region 검사취합

                case "idm011":
                    if (e.Link.ToLower() == "download")
                    {
                        var dataGridViewSelectedRowCollection = e.DataGridView.SelectedRows;
                        if (dataGridViewSelectedRowCollection.Count <= 0)
                        {
                            //선택 행 없음
                            System.Windows.Forms.MessageBox.Show
                                (
                                 "Không có tệp nào được chọn để lưu。\r\nNo Files have been selected to save."
                               , "", MessageBoxButtons.OK, MessageBoxIcon.Warning
                                );
                            return;
                        }

                        if (DialogResult.Yes
                            == System.Windows.Forms.MessageBox.Show
                                (
                                 "Bạn có muốn tải xuống tệp đã chọn không?\r\nWould you like to download the selected file?", ""
                               , MessageBoxButtons.YesNo
                                ))
                        {
                            var folderBrowserDialog = new FolderBrowserDialog();
                            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
                            var selectedPath = folderBrowserDialog.SelectedPath;

                            var query = new StringBuilder();
                            //일단 PCB만 Key판정(유니크)
                            //유니크하지않다면 비교해야할 키 추가 진행(date라던지 seq라던지)

                            foreach (DataGridViewRow dataGridViewSelectedRow in dataGridViewSelectedRowCollection)
                            {
                                string pcbId = dataGridViewSelectedRow.Cells["PcbId"].Value.ToString();
                                string tested = dataGridViewSelectedRow.Cells["Tested"].Value.ToString();

                                query.AppendLine
                                    (
                                     $@"
                                SELECT RIGHT([Source], CHARINDEX('\', REVERSE([Source]))-1) AS Filename
                                 , E_RawData
                                  FROM Y2sVn1iData.dbo.ICT_Test
                                 WHERE PcbId = '{pcbId}' AND Tested = '{tested}'
                                ;
                               "
                                    );
                            }

                            try
                            {
                                if (!(DbAccess.Default.GetDataSet(query.ToString()) is DataSet dataSet))
                                {
                                    //조회 된게 없음
                                    return;
                                }

                                foreach (DataTable dataTable in dataSet.Tables)
                                {
                                    if (dataTable.Rows.Count <= 0)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Tìm kiếm thất bại(Query failed.)");
                                        return;
                                    }

                                    //일단 수정사항에 Source가 파일명으로 대체된다고 써있으니 작성진행
                                    //아니면 파일명에 맞게 변경
                                    //확장자는 필수(.csv)
                                    foreach (DataRow dataRow in dataTable.Rows)
                                    {
                                        var fileName = dataRow["FileName"].ToString();
                                        var dataBytes = (byte[]) dataRow["E_RawData"];
                                        using (var fileStream = new FileStream
                                                   (
                                                    $"{selectedPath}/{fileName}"
                                                  , FileMode.Create
                                                   ))
                                        {
                                            fileStream.Write(dataBytes, 0, dataBytes.Length);
                                            fileStream.Close();
                                        }
                                    }
                                }
                            }
                            catch (SqlException sqlException)
                            {
                                //Message DB Error
                                System.Windows.Forms.MessageBox.Show($"{sqlException.Message}");
                                throw;
                            }
                            catch (IOException iOException)
                            {
                                //보통 파일이 생성되어 있거나, 파일 사용중일때 발생함
                                //자세한건 마소로...
                                System.Windows.Forms.MessageBox.Show($"{iOException.Message}");
                                return;
                            }
                            catch (Exception exception)
                            {
                                System.Windows.Forms.MessageBox.Show($"{exception.Message}");
                                return;
                            }

                            // Message 성공
                            System.Windows.Forms.MessageBox.Show($"Đã lưu\r\nSave complete");
                        }
                    }

                    break;
                case "idm012":
                    if (e.Link.ToLower() == "download")
                    {
                        var dataGridViewSelectedRowCollection = e.DataGridView.SelectedRows;
                        if (dataGridViewSelectedRowCollection.Count <= 0)
                        {
                            //선택 행 없음
                            System.Windows.Forms.MessageBox.Show
                                (
                                 "Không có tệp nào được chọn để lưu。\r\nNo Files have been selected to save."
                               , "", MessageBoxButtons.OK, MessageBoxIcon.Warning
                                );
                            return;
                        }

                        if (DialogResult.Yes
                            == System.Windows.Forms.MessageBox.Show
                                (
                                 "Bạn có muốn tải xuống tệp đã chọn không?\r\nWould you like to download the selected file?", ""
                               , MessageBoxButtons.YesNo
                                ))
                        {
                            var folderBrowserDialog = new FolderBrowserDialog();
                            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
                            var selectedPath = folderBrowserDialog.SelectedPath;

                            var query = new StringBuilder();
                            //일단 PCB만 Key판정(유니크)
                            //유니크하지않다면 비교해야할 키 추가 진행(date라던지 seq라던지)

                            foreach (DataGridViewRow dataGridViewSelectedRow in dataGridViewSelectedRowCollection)
                            {
                                string pcbId = dataGridViewSelectedRow.Cells["PcbId"].Value.ToString();
                                string tested = dataGridViewSelectedRow.Cells["Tested"].Value.ToString();

                                query.AppendLine
                                    (
                                     $@"
                                SELECT RIGHT([Source], CHARINDEX('\', REVERSE([Source]))-1) AS Filename
                                 , E_RawData
                                  FROM Y2sVn1iData.dbo.First_Func_Test
                                 WHERE PcbId = '{pcbId}' AND Tested = '{tested}'
                                ;
                               "
                                    );
                            }

                            try
                            {
                                if (!(DbAccess.Default.GetDataSet(query.ToString()) is DataSet dataSet))
                                {
                                    //조회 된게 없음
                                    return;
                                }

                                foreach (DataTable dataTable in dataSet.Tables)
                                {
                                    if (dataTable.Rows.Count <= 0)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Tìm kiếm thất bại(Query failed.)");
                                        return;
                                    }

                                    //일단 수정사항에 Source가 파일명으로 대체된다고 써있으니 작성진행
                                    //아니면 파일명에 맞게 변경
                                    //확장자는 필수(.csv)
                                    foreach (DataRow dataRow in dataTable.Rows)
                                    {
                                        var fileName = dataRow["FileName"].ToString();
                                        var dataBytes = (byte[]) dataRow["E_RawData"];
                                        using (var fileStream = new FileStream
                                                   (
                                                    $"{selectedPath}/{fileName}"
                                                  , FileMode.Create
                                                   ))
                                        {
                                            fileStream.Write(dataBytes, 0, dataBytes.Length);
                                            fileStream.Close();
                                        }
                                    }
                                }
                            }
                            catch (SqlException sqlException)
                            {
                                //Message DB Error
                                System.Windows.Forms.MessageBox.Show($"{sqlException.Message}");
                                throw;
                            }
                            catch (IOException iOException)
                            {
                                //보통 파일이 생성되어 있거나, 파일 사용중일때 발생함
                                //자세한건 마소로...
                                System.Windows.Forms.MessageBox.Show($"{iOException.Message}");
                                return;
                            }
                            catch (Exception exception)
                            {
                                System.Windows.Forms.MessageBox.Show($"{exception.Message}");
                                return;
                            }

                            // Message 성공
                            System.Windows.Forms.MessageBox.Show($"Đã lưu\r\nSave complete");
                        }
                    }

                    break;
                case "idm013":
                    break;
                case "idm014":
                    if (e.Link.ToLower() == "download")
                    {
                        var dataGridViewSelectedRowCollection = e.DataGridView.SelectedRows;
                        if (dataGridViewSelectedRowCollection.Count <= 0)
                        {
                            //선택 행 없음
                            System.Windows.Forms.MessageBox.Show
                                (
                                 "Không có tệp nào được chọn để lưu。\r\nNo Files have been selected to save."
                               , "", MessageBoxButtons.OK, MessageBoxIcon.Warning
                                );
                            return;
                        }

                        if (DialogResult.Yes
                            == System.Windows.Forms.MessageBox.Show
                                (
                                 "Bạn có muốn tải xuống tệp đã chọn không?\r\nWould you like to download the selected file?", ""
                               , MessageBoxButtons.YesNo
                                ))
                        {
                            var folderBrowserDialog = new FolderBrowserDialog();
                            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
                            var selectedPath = folderBrowserDialog.SelectedPath;

                            var query = new StringBuilder();
                            //일단 PCB만 Key판정(유니크)
                            //유니크하지않다면 비교해야할 키 추가 진행(date라던지 seq라던지)

                            foreach (DataGridViewRow dataGridViewSelectedRow in dataGridViewSelectedRowCollection)
                            {
                                string pcbId = dataGridViewSelectedRow.Cells["PcbId"].Value.ToString();
                                string tested = dataGridViewSelectedRow.Cells["Tested"].Value.ToString();

                                query.AppendLine
                                    (
                                     $@"
                                SELECT RIGHT([Source], CHARINDEX('\', REVERSE([Source]))-1) AS Filename
                                 , E_RawData
                                  FROM Y2sVn1iData.dbo.Second_Func_Test
                                 WHERE PcbId = '{pcbId}' AND Tested = '{tested}'
                                ;
                               "
                                    );
                            }

                            try
                            {
                                if (!(DbAccess.Default.GetDataSet(query.ToString()) is DataSet dataSet))
                                {
                                    //조회 된게 없음
                                    return;
                                }

                                foreach (DataTable dataTable in dataSet.Tables)
                                {
                                    if (dataTable.Rows.Count <= 0)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Tìm kiếm thất bại(Query failed.)");
                                        return;
                                    }

                                    //일단 수정사항에 Source가 파일명으로 대체된다고 써있으니 작성진행
                                    //아니면 파일명에 맞게 변경
                                    //확장자는 필수(.csv)
                                    foreach (DataRow dataRow in dataTable.Rows)
                                    {
                                        var fileName = dataRow["FileName"].ToString();
                                        var dataBytes = (byte[]) dataRow["E_RawData"];
                                        using (var fileStream = new FileStream
                                                   (
                                                    $"{selectedPath}/{fileName}"
                                                  , FileMode.Create
                                                   ))
                                        {
                                            fileStream.Write(dataBytes, 0, dataBytes.Length);
                                            fileStream.Close();
                                        }
                                    }
                                }
                            }
                            catch (SqlException sqlException)
                            {
                                //Message DB Error
                                System.Windows.Forms.MessageBox.Show($"{sqlException.Message}");
                                throw;
                            }
                            catch (IOException iOException)
                            {
                                //보통 파일이 생성되어 있거나, 파일 사용중일때 발생함
                                //자세한건 마소로...
                                System.Windows.Forms.MessageBox.Show($"{iOException.Message}");
                                return;
                            }
                            catch (Exception exception)
                            {
                                System.Windows.Forms.MessageBox.Show($"{exception.Message}");
                                return;
                            }

                            // Message 성공
                            System.Windows.Forms.MessageBox.Show($"Đã lưu\r\nSave complete");
                        }
                    }

                    break;

                #endregion

                case "jig301":
                    switch (e.Link.ToLower())
                    {
                        case "repairstart" when e.DataGridView.Rows.Count == 0:
                            System.Windows.Forms.MessageBox.Show("Error, is not infomation.");
                            return;
                        case "repairstart":
                        {
                            string SearchQuery = " Select * From JigRepairHist Where Jig = N'" + e.DataGridView.CurrentRow.Cells["Jig"].Value.ToString() + "' and Status = 1 ";
                            DataTable dt = DbAccess.Default.GetDataTable(SearchQuery);

                            if (dt.Rows.Count == 0)
                            {
                                JigRepairStart JigRepairStart = new JigRepairStart(e);
                                JigRepairStart.ShowDialog();
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Error, this jig is Confirm stand by.");
                                return;
                            }

                            e.AfterRefresh = WeRefreshPanel.Parent;
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "repairconfirm":
                        {
                            JigConfirm JigConfirm = new JigConfirm(e);
                            JigConfirm.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Parent;
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;


                case "wms001":
                    if (e.Link.ToLower() == "barcodeprint")
                    {
                        WMS.NewBarcode nb = new WMS.NewBarcode();
                        nb.ShowDialog();
                    }
                    else if (e.Link.ToLower() == "barcodereturn")
                    {
                        WMS.ReturnBarcodeHistory nr = new WMS.ReturnBarcodeHistory();
                        nr.ShowDialog();
                    }

                    break;
                case "wms002":
                    switch (e.Link.ToLower())
                    {
                        case "print_pcb":
                        {
                            WMS.Print_PCB pp = new WMS.Print_PCB();
                            pp.Text = "Chương trình in Label Barcode PCB (PCB Barcode Label Printing Program)";
                            pp.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            e.AfterRefresh = WeRefreshPanel.Parent;
                            // WMS.Print_PCB_V2 pp = new WMS.Print_PCB_V2();
                            // pp.Text = "PCB条码标签打印程序 (PCB Barcode Label Printing Program(ZPL))";
                            // pp.ShowDialog();
                            // e.AfterRefresh = WeRefreshPanel.Current;
                            // e.AfterRefresh = WeRefreshPanel.Parent;
                            break;
                        }
                        case "reprint":
                        {
                            WMS.Reprint_PCB rpp = new WMS.Reprint_PCB();
                            rpp.Text = "PCB Barcode Reprint Program";
                            rpp.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "reprint_master":
                            {
                                WMS.Reprint_PCB_M rpp_M = new WMS.Reprint_PCB_M();
                                rpp_M.Text = "PCB Barcode Reprint Program Master";
                                rpp_M.ShowDialog();
                                e.AfterRefresh = WeRefreshPanel.Current;
                                break;
                            }
                    }

                    break;

                case "wms013":
                    switch (e.Link.ToLower())
                    {
                        case "stockadjust":
                        {
                            var PI_frmMain00 = new PI_frmMain00();
                            PI_frmMain00.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            e.AfterRefresh = WeRefreshPanel.Parent;
                            break;
                        }
                    }

                    break;

                case "jig000":
                    switch (e.Link.ToLower())
                    {
                        case "recipient":
                        {
                            Recipient Recipient = new Recipient();
                            Recipient.ShowDialog();
                            break;
                        }
                    }

                    break;
                case "prm001":
                    switch (e.Link.ToLower())
                    {
                        case "edit":
                        {
                            WiseMEdit.EditForm prm001Process = new WiseMEdit.EditForm("Division", e);
                            prm001Process.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;

                case "prm105":
                    switch (e.Link.ToLower())
                    {
                        case "editlocationgroup":
                        {
                            WiseMEdit.EditForm prm105Process = new WiseMEdit.EditForm("Rm_Location_Group", e);
                            prm105Process.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "editlocation":
                        {
                            WiseMEdit.EditForm prm105Process = new WiseMEdit.EditForm("Rm_Location", e);
                            prm105Process.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "printpalletlocation":
                            {
                                var selectRowIndexList = new List<DataGridViewRow>();
                                foreach (DataGridViewRow row in e.DataGridView.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (!cell.Selected) continue;
                                        selectRowIndexList.Add(row);
                                        break;
                                    }
                                }

                                if (selectRowIndexList.Count == 0)
                                {
                                    return;
                                }

                                if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show($"Are you sure?", "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                                {
                                    break;
                                }

                                var cBarcode = new clsBarcode.clsBarcode();
                                var bcdName = "PalletLocationLabel";
                                var bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='{bcdName}'");
                                cBarcode.LoadFromXml(bcdData.ToString());
                                var labelCount = 6;
                                for (var i = 0; i < selectRowIndexList.Count / labelCount + 1; i++)
                                {
                                    for (var j = 1; j <= labelCount; j++)
                                    {
                                        var index = i * labelCount + j - 1;
                                        var value = index < selectRowIndexList.Count ? selectRowIndexList[index].Cells["Location"].Value.ToString() : string.Empty;
                                        cBarcode.Data.SetText($"BARCODE{j}", value);
                                    }
                                    cBarcode.Data.AddLabel();
                                }
                                cBarcode.Print(false);
                                break;
                            }
                        case "printlocation":
                            {
                                var selectRowIndexList = new List<DataGridViewRow>();
                                foreach (DataGridViewRow row in e.DataGridView.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (!cell.Selected) continue;
                                        selectRowIndexList.Add(row);
                                        break;
                                    }
                                }

                                if (selectRowIndexList.Count == 0)
                                {
                                    return;
                                }

                                if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show($"Are you sure?", "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                                {
                                    break;
                                }

                                var cBarcode = new clsBarcode.clsBarcode();
                                var bcdName = "LocationLabel";
                                var bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='{bcdName}'");
                                cBarcode.LoadFromXml(bcdData.ToString());
                                foreach (var row in selectRowIndexList)
                                {
                                    cBarcode.Data.SetText("BARCODE", row.Cells["Location"].Value.ToString());
                                    cBarcode.Data.SetText("BARCODETEXT", row.Cells["Location"].Value.ToString());
                                    cBarcode.Data.AddLabel();
                                }

                                cBarcode.Print(false);
                                break;
                            }
                    }

                    break;
                case "prm002":
                    switch (e.Link.ToLower())
                    {
                        case "edit":
                        {
                            //EditForm prm002Process = new EditForm(e);
                            //prm002Process.ShowDialog();
                            //e.AfterRefresh = WeRefreshPanel.Current;
                            WiseMEdit.EditForm RoutingEdit = new WiseMEdit.EditForm("Routing", e);
                            RoutingEdit.ShowDialog();
                            break;
                        }
                    }

                    break;

                case "prm003":
                    switch (e.Link.ToLower())
                    {
                        case "edit":
                        {
                            WiseMEdit.EditForm prm003Process = new WiseMEdit.EditForm("WorkCenter", e);
                            prm003Process.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;

                case "dgs203":
                    switch (e.Link.ToLower())
                    {
                        case "searchingdata":
                        {
                            DataSearching DataSearching = new DataSearching(e);
                            DataSearching.ShowDialog();
                            break;
                        }
                    }

                    break;

                case "dgs210":
                    if (e.Link.ToLower() == "searchingdata")
                    {
                        DataSearching DataSearching = new DataSearching(e);
                        DataSearching.ShowDialog();
                    }

                    break;

                case "dgs204":
                    if (e.Link.ToLower() == "searchingdata")
                    {
                        DataSearching DataSearching = new DataSearching(e);
                        DataSearching.ShowDialog();
                    }

                    break;

                case "prm101":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm101Process = new WiseMEdit.EditForm("Material", e);
                        prm101Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm102":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm102Process = new WiseMEdit.EditForm("Material Mapping", e);
                        prm102Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm104":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm104Process = new WiseMEdit.EditForm("RawMaterial", e);
                        prm104Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "wip511":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm wip511Process = new WiseMEdit.EditForm("RawMaterial_Hist", e);
                        wip511Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm103":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm103Process = new EditForm(e);
                        prm103Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm201":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm201Process = new WiseMEdit.EditForm("Bad", e);
                        prm201Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm202":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm202Process = new WiseMEdit.EditForm("Notwork", e);
                        prm202Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm203":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm203Process = new EditForm(e);
                        prm203Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm204":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm204Process = new EditForm(e);
                        prm204Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm205":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm205Process = new WiseMEdit.EditForm("Repair", e);
                        prm205Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm303":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm303Process = new EditForm(e);
                        prm303Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm302":
                    if (e.Link.ToLower() == "edit")
                    {
                        Edit_Worker Edit_Worker = new Edit_Worker(e);
                        Edit_Worker.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }
                    else if (e.Link.ToLower() == "setfalse")
                    {
                        WorkerSetToFalse startSetFalse = new WorkerSetToFalse();
                        startSetFalse.ProcessStart(e);
                    }

                    break;

                case "prm304":
                    if (e.Link.ToLower() == "edit")
                    {
                        WiseMEdit.EditForm prm304Process = new WiseMEdit.EditForm("WorkTeam", e);
                        prm304Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm401":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm401Process = new EditForm(e);
                        prm401Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "prm501":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm prm501Process = new EditForm(e);
                        prm501Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "wom001":
                    switch (e.Link.ToLower())
                    {
                        case "closed":
                        {
                            var endWorkOrderConfirm = new EndWorkOrderConfirm();
                            endWorkOrderConfirm.ProcessStart(e);
                            break;
                        }
                        case "closecancel":
                        {
                            var endWorkOrderCancelConfirm = new EndWorkOrderCancelConfirm();
                            endWorkOrderCancelConfirm.ProcessStart(e);
                            break;
                        }
                        case "qtychange":
                        {
                            ChangeCount ChangeCount = new ChangeCount(e);
                            ChangeCount.ShowDialog();
                            break;
                        }
                        case "delete":
                        {
                            var deleteWorkOrderConfirm = new DeleteWorkOrderConfirm();
                            deleteWorkOrderConfirm.ProcessStart(e);
                            break;
                        }
                        case "edit":
                            {
                                WiseMEdit.EditForm wom001_Edit_Process = new WiseMEdit.EditForm("Workorder", e);
                                wom001_Edit_Process.ShowDialog();
                                e.AfterRefresh = WeRefreshPanel.Current;
                                break;
                            }
                    }

                    break;

                case "wip001":
                    if (e.Link.ToLower() == "wipinfo")
                    {
                        WipInfo wip001Process = new WipInfo(e);
                        wip001Process.ShowDialog();
                    }

                    break;

                // H/S Receipt
                case "prd801":
                    if (e.Link.ToLower() == "input h/s receipt")
                    {
                        HS_Receipt_frmMain00 hs_Receipt = new HS_Receipt_frmMain00();
                        hs_Receipt.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "qis001":
                    switch (e.Link.ToLower())
                    {
                        case "badinput":
                            BadInput badProcess = new BadInput();
                            badProcess.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        case "badedit":
                            BadEdit badEdit = new BadEdit(e.DataGridView.CurrentRow.Cells["BadHist"].Value.ToString());
                            badEdit.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                    }

                    break;
                case "qis601":
                    if (e.Link.ToLower() == "repairinput")
                    {
                        RepairInput repairProcess = new RepairInput(e);
                        repairProcess.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "rep001":
                    switch (e.Panel)
                    {
                        case "Panel0":
                            switch (e.Link.ToLower())
                            {
                                case "repair":
                                    RepairInput repairInput = new RepairInput(e);
                                    repairInput.ShowDialog();
                                    e.AfterRefresh = WeRefreshPanel.Current;
                                    break;
                            }
                            break;
                        case "Panel1":
                            switch (e.Link.ToLower())
                            {
                                case "repairedit":
                                    RepairEdit repairEdit = new RepairEdit(e);
                                    repairEdit.ShowDialog();
                                    e.AfterRefresh = WeRefreshPanel.Current;
                                    break;
                            }
                            break;
                    }
                    break;

                case "wpm001":
                    if (e.Link.ToLower() == "edit")
                    {
                        EditForm wpm001Process = new EditForm(e);
                        wpm001Process.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "wip701":
                    if (e.Link.ToLower() == "blocking_rm")
                    {
                        Blocking blocking_RM = new Blocking("RM", e);
                        blocking_RM.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "pn201":
                    if (e.Link.ToLower() == "blocking_pack")
                    {
                        Blocking blocking_PACK = new Blocking("PACK", e);
                        blocking_PACK.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "pn401":
                    if (e.Link.ToLower() == "derived product input")
                    {
                        DerivedProduct_frmMain00 dp = new DerivedProduct_frmMain00();
                        dp.ShowDialog();

                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                case "pn501":
                    switch (e.Link.ToLower())
                    {
                        case "repacking":
                        {
                            RePacking rePacking = new RePacking();
                            rePacking.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "reprint":
                        {
                            RepackingReprint repackingReprint = new RepackingReprint();
                            repackingReprint.ShowDialog();
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                case "pn601":
                    if (e.Link == "PalletCheck")
                    {
                        var palletCheck = new PalletCheck();
                        palletCheck.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "spc002":
                    if (e.Link.ToLower().Equals("edit"))
                    {
                        SPC002 spc002 = new SPC002(e);
                        spc002.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "spc003":
                    if (e.Link.ToLower().Equals("edit"))
                    {
                        SPC003 spc003 = new SPC003(e);
                        spc003.ShowDialog();
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;

                case "spc004":
                    if (e.Link.ToLower().Equals("spc_test"))
                    {
                        System.Diagnostics.Process.Start
                            (
                             "SPC_ChartFX7.exe"
                           , System.Windows.Forms.Application.StartupPath
                            );
                    }
                    else
                    {
                        string script = "select MinValue, MaxValue from SpcItems "
                                        + " where SpcItem = N'"
                                        + e.DataGridView.CurrentRow.Cells["spcitem"].Value.ToString()
                                        + "' "
                                        + "       And ItemType = '"
                                        + e.DataGridView.CurrentRow.Cells["ItemType"].Value.ToString()
                                        + "' "
                                        + "       And Model = '"
                                        + e.DataGridView.CurrentRow.Cells["Model"].Value.ToString()
                                        + "' "
                                        + "       And InspType = '"
                                        + e.DataGridView.CurrentRow.Cells["InspType"].Value.ToString()
                                        + "' ";
                        DataTable minmax = DbAccess.Default.GetDataTable(script);

                        // SpcCl 조회 - ybkim
                        string[] tempString = e.Script.Split('\n');
                        tempString = tempString[5].Split('\'');
                        string spcDate = tempString[1];
                        DateTime tempDateTime = DateTime.Parse(spcDate);
                        tempDateTime = tempDateTime.AddMonths(-1);
                        spcDate = tempDateTime.ToString("yyyy-MM");
                        tempString[1] = spcDate;
                        string spcClscript = "select * from SpcCl "
                                             + " where SpcDate = '"
                                             + spcDate
                                             + "' "
                                             + "       And SpcItem = N'"
                                             + tempString[3].ToString()
                                             + "' "
                                             + "       And ItemType = '"
                                             + tempString[5].ToString()
                                             + "' "
                                             + "       And Model = '"
                                             + tempString[7].ToString()
                                             + "' "
                                             + "       And InspType = '"
                                             + tempString[9].ToString()
                                             + "' ";
                        DataTable spcClDataTable = DbAccess.Default.GetDataTable(spcClscript);

                        if (spcClDataTable.Rows.Count > 0) // SpcCl 조회 결과 유무
                        {
                            if (minmax.Rows.Count > 0)
                            {
                                if (e.Link.ToLower().Equals("chart"))
                                {
                                    chart chart = new chart(e, spcClDataTable);
                                    chart.ShowDialog();
                                }
                                else if (e.Link.ToLower().Equals("cp/cpk"))
                                {
                                    chart chart = new chart(e, spcClDataTable);
                                    chart.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("You check spc basicdata.", "Warning", MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show
                                (
                                 "Check input 'CL' last month data. ", "Warning"
                               , MessageBoxButtons.OK, MessageBoxIcon.Warning
                                );
                            if (dr == DialogResult.OK)
                            {
                                SPC.Spc005 spc005 = new SPC.Spc005(e, tempString);
                                spc005.ShowDialog();
                            }
                        }
                    }


                    break;

                #region 체크시트

                // 기준정보 - 라인명관리
                case "cst001":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            WiseMEdit.EditForm eForm = new WiseMEdit.EditForm("CsLineRoute", e);
                            eForm.ShowDialog();

                            //EditForm cst001Process = new EditForm(e);
                            //cst001Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            WiseMEdit.EditForm eForm = new WiseMEdit.EditForm("CsLineRoute", e);
                            eForm.ShowDialog();

                            //EditForm cst001Process = new EditForm(e);
                            //cst001Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 기준정보 - 체크시트관리
                case "cst002":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst002Process = new EditForm(e);
                            cst002Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            WiseMEdit.EditForm eForm = new WiseMEdit.EditForm("CsSpec", e);
                            eForm.ShowDialog();

                            //EditForm cst002Process = new EditForm(e);
                            //cst002Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 일일장비점검 사양관리
                case "cst003":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst003Process = new EditForm(e);
                            cst003Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            EditForm cst003Process = new EditForm(e);
                            cst003Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 정기장비점검 사양관리
                case "cst004":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst004Process = new EditForm(e);
                            cst004Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            EditForm cst004Process = new EditForm(e);
                            cst004Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 설비예방점검 사양관리
                case "cst005":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst005Process = new EditForm(e);
                            cst005Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            EditForm cst005Process = new EditForm(e);
                            cst005Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 연간 Overhaul 사양관리
                case "cst006":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst006Process = new EditForm(e);
                            cst006Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            EditForm cst006Process = new EditForm(e);
                            cst006Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 3정5행 사양관리
                case "cst007":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst007Process = new EditForm(e);
                            cst007Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            EditForm cst007Process = new EditForm(e);
                            cst007Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 체크시트 사양관리
                case "cst008":
                    switch (e.Link.ToLower())
                    {
                        // /Parameter [CsCode:'CsCode'] Parameter/
                        case "insert" when string.IsNullOrEmpty(e.Script) == false:
                        {
                            Checksheet.CsCheckSheetSpecDetail specDetail
                                = new Checksheet.CsCheckSheetSpecDetail(true, e);

                            if (specDetail.ShowDialog() == DialogResult.OK)
                                e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "insert":
                            MessageBox.Show
                                (
                                 "Please select after search parent panel.", "Not Valid"
                               , MessageBoxButtons.OK, MessageBoxIcon.Information
                                );
                            break;
                        case "update":
                        {
                            Checksheet.CsCheckSheetSpecDetail specDetail = new Checksheet.CsCheckSheetSpecDetail(false, e);

                            if (specDetail.ShowDialog() == DialogResult.OK)
                                e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete":
                        {
                            Checksheet.CsCheckSheetSpecDetail specDetail = new Checksheet.CsCheckSheetSpecDetail();

                            if (specDetail.Delete(e) == true)
                            {
                                e.AfterRefresh = WeRefreshPanel.Current;
                            }

                            break;
                        }
                    }

                    break;
                // 파라매터 사양관리
                case "cst009":
                    switch (e.Link.ToLower())
                    {
                        case "insert":
                        {
                            EditForm cst009Process = new EditForm(e);
                            cst009Process.ShowDialog();
                            break;
                        }
                        case "update":
                        {
                            WiseMEdit.EditForm eForm = new WiseMEdit.EditForm("CsParameterCheckSpec", e);
                            eForm.ShowDialog();

                            //EditForm cst002Process = new EditForm(e);
                            //cst002Process.ShowDialog();
                            break;
                        }
                    }

                    break;
                // 일일장비점검 실적현황
                case "cst101":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string UpdatetQuery = "Update CsDailyHist Set "
                                                  + " confirmed=getdate()"
                                                  + ",Confirmer='"
                                                  + WiseApp.Id
                                                  + "'"
                                                  + " Where CsDate='"
                                                  + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                     .Substring(0, 10) //"yyyy-MM-dd"
                                                  + "' and Line ='"
                                                  + e.DataGridView.CurrentRow.Cells["Line"].Value
                                                     .ToString()
                                                  + "' and CsShift ='"
                                                  + e.DataGridView.CurrentRow.Cells["CsShift"].Value
                                                     .ToString()
                                                  + "' and (Confirmer='' or Confirmer is null)";

                            e.DbAccess.ExecuteQuery(UpdatetQuery);

                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete":
                        {
                            string DeleteQuery = "Delete From CsDailyHist "
                                                 + " Where CsDate='"
                                                 + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                    .Substring(0, 10) //"yyyy-MM-dd"
                                                 + "' and Line ='"
                                                 + e.DataGridView.CurrentRow.Cells["Line"].Value
                                                    .ToString()
                                                 + "' and CsShift ='"
                                                 + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString()
                                                 + "'";

                            e.DbAccess.ExecuteQuery(DeleteQuery);

                            WiseM.MessageBox.Show("이력삭제 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                // 정기장비점검 실적현황
                case "cst102":
                    if (e.Link.ToLower() == "managercheck")
                    {
                        string UpdatetQuery;
                        string DTQuery
                            = "select (select Checkperiod from CsPeriodicSpec where Line=c.Line and Seq=c.Seq) as Checkperiod,(select PlannedDate from CsPeriodicSpec where Line=c.Line and Seq=c.Seq) as PlannedDate,* from CsPeriodicHist c Where CsDate='"
                              + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10)
                              + "' and Line ='"
                              + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString()
                              + "' and (Confirmer='' or Confirmer is null)";


                        DataTable specDT = e.DbAccess.GetDataTable(DTQuery);

                        for (int i = 0; i < specDT.Rows.Count; i++)
                        {
                            string strDate
                                = specDT.Rows[i]["PlannedDate"].ToString().Substring(0, 10); //가져온 PlannedDate

                            DateTime convDate = DateTime.Now;

                            switch (specDT.Rows[i]["CheckPeriod"].ToString())
                            {
                                ////   --2-Weekly / 3_Monthly / 3-Monthly / 6_Monthly / 6-Monthly / Monthly / Yearly
                                case "2-Weekly":
                                    convDate = DateTime.Parse(strDate).AddDays(14);
                                    break;
                                case "3_Monthly":
                                    convDate = DateTime.Parse(strDate).AddMonths(3);
                                    break;
                                case "3-Monthly":
                                    convDate = DateTime.Parse(strDate).AddMonths(3);
                                    break;
                                case "6_Monthly":
                                    convDate = DateTime.Parse(strDate).AddMonths(6);
                                    break;
                                case "6-Monthly":
                                    convDate = DateTime.Parse(strDate).AddMonths(6);
                                    break;
                                case "Monthly":
                                    convDate = DateTime.Parse(strDate).AddMonths(1);
                                    break;
                                case "Yearly":
                                    convDate = DateTime.Parse(strDate).AddYears(1);
                                    break;
                                default:
                                    break;
                            }

                            if (specDT.Rows[i]["Values"].ToString() == "")
                            {
                            }
                            else
                            {
                                UpdatetQuery = "Update CsPeriodicHist Set "
                                               + " confirmed=getdate()"
                                               + ",Confirmer='"
                                               + WiseApp.Id
                                               + "'" //ManagerCheck
                                               + " Where CsDate='"
                                               + specDT.Rows[i]["CsDate"].ToString().Substring(0, 10)
                                               + "' and Line ='"
                                               + specDT.Rows[i]["Line"].ToString()
                                               + "' and Seq='"
                                               + specDT.Rows[i]["Seq"].ToString()
                                               + "'";

                                UpdatetQuery += "Update  CsPeriodicSpec set PlannedDate = '" + convDate.ToString().Substring(0, 10) + "' Where Line ='" + specDT.Rows[i]["Line"].ToString() + "' and Seq='" + specDT.Rows[i]["Seq"].ToString() + "'";
                                e.DbAccess.ExecuteQuery(UpdatetQuery);
                            }
                        }

                        WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);
                        e.AfterRefresh = WeRefreshPanel.Current;
                    }

                    break;
                // 3정5행 실적현황
                case "cst103":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string UpdatetQuery = "Update Cs3c5sHist Set "
                                                  + " confirmed=getdate()"
                                                  + ",Confirmer='"
                                                  + WiseApp.Id
                                                  + "'"
                                                  + " Where CsDate='"
                                                  + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                     .Substring(0, 10) //"yyyy-MM-dd"
                                                  + "' and Line ='"
                                                  + e.DataGridView.CurrentRow.Cells["Line"].Value
                                                     .ToString()
                                                  + "' and  (Confirmer='' or Confirmer is null)";

                            e.DbAccess.ExecuteQuery(UpdatetQuery);

                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete":
                        {
                            string DeleteQuery = "Delete From Cs3c5sHist "
                                                 + " Where CsDate='"
                                                 + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                    .Substring(0, 10) //"yyyy-MM-dd"
                                                 + "' and Line ='"
                                                 + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString()
                                                 + "'";

                            e.DbAccess.ExecuteQuery(DeleteQuery);

                            WiseM.MessageBox.Show("이력삭제 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                // 예방보전 실적현황
                case "cst104":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string UpdatetQuery;
                            string DTQuery
                                = "select (select Checkperiod from CsPreventSpec where Line=c.Line and Seq=c.Seq) as Checkperiod,(select PlannedDate from CsPreventSpec where Line=c.Line and Seq=c.Seq) as PlannedDate,* from CsPreventHist c Where CsDate='"
                                  + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10)
                                  + "' and Line ='"
                                  + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString()
                                  + "' and (Confirmer='' or Confirmer is null)";


                            DataTable specDT = e.DbAccess.GetDataTable(DTQuery);

                            for (int i = 0; i < specDT.Rows.Count; i++)
                            {
                                string strDate
                                    = specDT.Rows[i]["PlannedDate"].ToString().Substring(0, 10); //가져온 PlannedDate

                                DateTime convDate = DateTime.Now;

                                switch (specDT.Rows[i]["CheckPeriod"].ToString())
                                {
                                    ////   --2-Weekly / 3_Monthly / 3-Monthly / 6_Monthly / 6-Monthly / Monthly / Yearly
                                    case "2-Weekly":
                                        convDate = DateTime.Parse(strDate).AddDays(14);
                                        break;
                                    case "3_Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(3);
                                        break;
                                    case "3-Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(3);
                                        break;
                                    case "6_Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(6);
                                        break;
                                    case "6-Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(6);
                                        break;
                                    case "Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(1);
                                        break;
                                    case "Yearly":
                                        convDate = DateTime.Parse(strDate).AddYears(1);
                                        break;
                                    default:
                                        break;
                                }

                                if (specDT.Rows[i]["Values"].ToString() == "")
                                {
                                }
                                else
                                {
                                    UpdatetQuery = "Update CsPreventHist Set "
                                                   + " confirmed=getdate()"
                                                   + ",Confirmer='"
                                                   + WiseApp.Id
                                                   + "'" //ManagerCheck
                                                   + " Where CsDate='"
                                                   + specDT.Rows[i]["CsDate"].ToString().Substring(0, 10)
                                                   + "' and Line ='"
                                                   + specDT.Rows[i]["Line"].ToString()
                                                   + "' and Seq='"
                                                   + specDT.Rows[i]["Seq"].ToString()
                                                   + "'";

                                    UpdatetQuery += "Update  CsPreventSpec set PlannedDate = '" + convDate.ToString().Substring(0, 10) + "' Where Line ='" + specDT.Rows[i]["Line"].ToString() + "' and Seq='" + specDT.Rows[i]["Seq"].ToString() + "'";
                                    e.DbAccess.ExecuteQuery(UpdatetQuery);
                                }
                            }

                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                // Overhaul 실적현황
                case "cst105":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string UpdatetQuery;
                            string DTQuery
                                = "select (select Checkperiod from CsOverhaulSpec where Line=c.Line and Seq=c.Seq) as Checkperiod,(select PlannedDate from CsOverhaulSpec where Line=c.Line and Seq=c.Seq) as PlannedDate,* from CsOverhaulHist c Where CsDate='"
                                  + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10)
                                  + "' and Line ='"
                                  + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString()
                                  + "' and (Confirmer='' or Confirmer is null)";


                            DataTable specDT = e.DbAccess.GetDataTable(DTQuery);

                            for (int i = 0; i < specDT.Rows.Count; i++)
                            {
                                string strDate
                                    = specDT.Rows[i]["PlannedDate"].ToString().Substring(0, 10); //가져온 PlannedDate

                                DateTime convDate = DateTime.Now;

                                switch (specDT.Rows[i]["CheckPeriod"].ToString())
                                {
                                    ////   --2-Weekly / 3_Monthly / 3-Monthly / 6_Monthly / 6-Monthly / Monthly / Yearly
                                    case "2-Weekly":
                                        convDate = DateTime.Parse(strDate).AddDays(14);
                                        break;
                                    case "3_Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(3);
                                        break;
                                    case "3-Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(3);
                                        break;
                                    case "6_Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(6);
                                        break;
                                    case "6-Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(6);
                                        break;
                                    case "Monthly":
                                        convDate = DateTime.Parse(strDate).AddMonths(1);
                                        break;
                                    case "Yearly":
                                        convDate = DateTime.Parse(strDate).AddYears(1);
                                        break;
                                    default:
                                        break;
                                }

                                if (specDT.Rows[i]["Values"].ToString() == "")
                                {
                                }
                                else
                                {
                                    UpdatetQuery = "Update CsOverhaulHist Set "
                                                   + " confirmed=getdate()"
                                                   + ",Confirmer='"
                                                   + WiseApp.Id
                                                   + "'" //ManagerCheck
                                                   + " Where CsDate='"
                                                   + specDT.Rows[i]["CsDate"].ToString().Substring(0, 10)
                                                   + "' and Line ='"
                                                   + specDT.Rows[i]["Line"].ToString()
                                                   + "' and Seq='"
                                                   + specDT.Rows[i]["Seq"].ToString()
                                                   + "'";

                                    UpdatetQuery += "Update  CsOverhaulSpec set PlannedDate = '" + convDate.ToString().Substring(0, 10) + "' Where Line ='" + specDT.Rows[i]["Line"].ToString() + "' and Seq='" + specDT.Rows[i]["Seq"].ToString() + "'";
                                    e.DbAccess.ExecuteQuery(UpdatetQuery);
                                }
                            }

                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                // 체크시트 실적현황
                case "cst106":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string query = string.Empty;

                            query += "\r\n";
                            query += "\r\n IF EXISTS (SELECT TOP 1 NULL FROM CsCheckSheet WHERE CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' AND CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' AND CsCode IN (SELECT CsCode FROM CsSpec WHERE Line = '" + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString() + "' GROUP BY CsCode) AND ISNULL(Confirmer, '') = '') ";
                            query += "\r\n BEGIN ";

                            query += "\r\n";
                            query
                                += "\r\n  INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Confirmed, Confirmer, Updated, Updater, [Type]) ";
                            query += "\r\n  SELECT  CsCode ";
                            query += "\r\n          ,CsDate ";
                            query += "\r\n          ,CsShift ";
                            query += "\r\n          ,Seq ";
                            query += "\r\n          ,CheckGroup ";
                            query += "\r\n          ,CheckItems ";
                            query += "\r\n          ,[Values] ";
                            query += "\r\n          ,Remark ";
                            query += "\r\n          ,Checked ";
                            query += "\r\n          ,Checker ";
                            query += "\r\n          ,GETDATE() ";
                            query += "\r\n          ,'" + WiseApp.Id + "' ";
                            query += "\r\n          ,Updated ";
                            query += "\r\n          ,Updater ";
                            query += "\r\n          ,[Type] ";
                            query += "\r\n  FROM    CsCheckSheet ";
                            query += "\r\n  WHERE   CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n      AND CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n      AND CsCode IN (SELECT CsCode FROM CsSpec WHERE Line = '" + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString() + "' GROUP BY CsCode) ";
                            query += "\r\n      AND ISNULL(Confirmer, '') = '' ";

                            query += "\r\n";
                            query += "\r\n  UPDATE    CsCheckSheet ";
                            query += "\r\n  SET       Confirmed = GETDATE() ";
                            query += "\r\n            ,Confirmer = '" + WiseApp.Id + "' ";
                            query += "\r\n  WHERE     CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n        AND CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n        AND CsCode IN (SELECT CsCode FROM CsSpec WHERE Line = '" + e.DataGridView.CurrentRow.Cells["Line"].Value.ToString() + "' GROUP BY CsCode) ";
                            query += "\r\n        AND ISNULL(Confirmer, '') = '' ";

                            query += "\r\n END ";

                            e.DbAccess.ExecuteQuery(query);

                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);

                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete":
                        {
                            string query = string.Empty;

                            query += "\r\n DELETE FROM CsCheckSheetHist ";
                            query += "\r\n WHERE  CsCheckSheetHist = ( ";
                            query += "\r\n                              SELECT  TOP 1 B.CsCheckSheetHist ";
                            query += "\r\n                              FROM    CsCheckSheetHist B ";
                            query += "\r\n                              WHERE   B.CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n                                  AND B.CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n                                  AND B.CsCode = '" + e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString() + "' ";
                            query += "\r\n                                  AND B.Seq = '" + e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString() + "' ";
                            query += "\r\n                              ORDER BY B.Updated DESC ";
                            query += "\r\n                           ) ";

                            query += "\r\n";
                            query += "\r\n IF EXISTS (SELECT TOP 1 NULL FROM CsCheckSheetHist B WHERE   B.CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n                                                          AND B.CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n                                                          AND B.CsCode = '" + e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString() + "' ";
                            query += "\r\n                                                          AND B.Seq = '" + e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString() + "' ";
                            query += "\r\n          ) ";
                            query += "\r\n BEGIN ";
                            query += "\r\n";
                            query += "\r\n      UPDATE   CsCheckSheet ";
                            query += "\r\n      SET      CheckGroup  = S.CheckGroup ";
                            query += "\r\n               ,CheckItems = S.CheckItems ";
                            query += "\r\n               ,[Values]   = S.[Values] ";
                            query += "\r\n               ,Remark     = S.Remark ";
                            query += "\r\n               ,Checked    = S.Checked ";
                            query += "\r\n               ,Checker    = S.Checker ";
                            query += "\r\n               ,Confirmed  = S.Confirmed ";
                            query += "\r\n               ,Confirmer  = S.Confirmer ";
                            query += "\r\n               ,Updated    = S.Updated ";
                            query += "\r\n               ,Updater    = S.Updater ";
                            query += "\r\n               ,[Type]     = S.[Type] ";
                            query += "\r\n      FROM     ( ";
                            query += "\r\n                SELECT  TOP 1 * ";
                            query += "\r\n                FROM    CsCheckSheetHist B ";
                            query += "\r\n                WHERE   B.CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n                    AND B.CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n                    AND B.CsCode = '" + e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString() + "' ";
                            query += "\r\n                    AND B.Seq = '" + e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString() + "' ";
                            query += "\r\n                ORDER BY B.Updated DESC ";
                            query += "\r\n               ) S ";
                            query += "\r\n      WHERE    CsCheckSheet.CsDate = S.CsDate ";
                            query += "\r\n           AND CsCheckSheet.CsShift = S.CsShift ";
                            query += "\r\n           AND CsCheckSheet.CsCode = S.CsCode ";
                            query += "\r\n           AND CsCheckSheet.Seq = S.Seq ";
                            query += "\r\n";
                            query += "\r\n END ";
                            query += "\r\n ELSE ";
                            query += "\r\n BEGIN ";
                            query += "\r\n";
                            query += "\r\n      DELETE FROM CsCheckSheet ";
                            query += "\r\n      WHERE   CsDate = '" + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString().Substring(0, 10) + "' ";
                            query += "\r\n          AND CsShift = '" + e.DataGridView.CurrentRow.Cells["CsShift"].Value.ToString() + "' ";
                            query += "\r\n          AND CsCode = '" + e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString() + "' ";
                            query += "\r\n          AND Seq = '" + e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString() + "' ";
                            query += "\r\n";
                            query += "\r\n END ";

                            e.DbAccess.ExecuteQuery(query);

                            WiseM.MessageBox.Show("이력삭제 완료", "안내", MessageBoxIcon.Information);

                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;
                // 파라미터 조건관리 실적현황
                case "cst107":
                    switch (e.Link.ToLower())
                    {
                        case "managercheck":
                        {
                            string UpdatetQuery = "Update CsParameterCheckHist Set "
                                                  + " confirmed=getdate()"
                                                  + ",Confirmer='"
                                                  + WiseApp.Id
                                                  + "'"
                                                  + " Where CsDate='"
                                                  + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                     .Substring(0, 10) //"yyyy-MM-dd"
                                                  + "' and CsCode ='"
                                                  + e.DataGridView.CurrentRow.Cells["CsCode"].Value
                                                     .ToString()
                                                  + "' and (Confirmer='' or Confirmer is null)";

                            e.DbAccess.ExecuteQuery(UpdatetQuery);


                            WiseM.MessageBox.Show("관리자확인 완료", "안내", MessageBoxIcon.Information);

                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                        case "delete":
                        {
                            string DeleteQuery = "Delete From CsParameterCheckHist "
                                                 + " Where CsDate='"
                                                 + e.DataGridView.CurrentRow.Cells["CsDate"].Value.ToString()
                                                    .Substring(0, 10) //"yyyy-MM-dd"
                                                 + "' and CsCode ='"
                                                 + e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString()
                                                 + "'";

                            e.DbAccess.ExecuteQuery(DeleteQuery);

                            WiseM.MessageBox.Show("이력삭제 완료", "안내", MessageBoxIcon.Information);
                            e.AfterRefresh = WeRefreshPanel.Current;
                            break;
                        }
                    }

                    break;

                #endregion
            }
        }


        public override WiseM.Forms.SkinForm GetCustomPanelLinkForm(CustomPanelLinkEventArgs e)
        {
            return new SkinForm();
        }

        public override void PanelOpened(PanelOpenedEventArgs e)
        {
            base.PanelOpened(e);
        }
    }
}
