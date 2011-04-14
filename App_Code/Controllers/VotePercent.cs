using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VotePercent
/// </summary>
public class VotePercent
{
    public VotePercent(int positivePercenet, int negativePercenet)
    {
        PositivePercent = positivePercenet;
        NegativePercent = negativePercenet;
    }
    public int PositivePercent { get; set; }
    public int NegativePercent { get; set; }
}
