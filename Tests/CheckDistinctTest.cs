using System.Collections.Generic;
using System.Linq;
using Models;
using Xunit;

namespace Tests;

public class CheckDistinctTest
{
    List<DistinctTypedModel> Values = new List<DistinctTypedModel>();
    public CheckDistinctTest()
    {
        Values.Add(new DistinctTypedModel { Value = 650, SubValue = 200});
        Values.Add(new DistinctTypedModel { Value = 650, SubValue = 312});
        Values.Add(new DistinctTypedModel { Value = 425, SubValue = 150});
        Values.Add(new DistinctTypedModel { Value = 800, SubValue = 317});
        Values.Add(new DistinctTypedModel { Value = 900, SubValue = 205});
    }

    [Fact]
    public void CheckItems()
    {
        int result = Values.Count() - Values.Select(x => x.SubValue).Distinct().Count();
        Assert.NotEqual(0, result);
    }
}