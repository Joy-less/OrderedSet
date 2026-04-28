using System.Collections;

namespace ExtendedCollections.Tests;

public class OrderedSetTests {
    [Fact]
    public void ReadmeTest() {
        OrderedSet<string> set = new();
        set.Add("pizza");
        set.Add("hotdog");
        set.Add("pizza");
        string setString = string.Join(", ", set);
        setString.ShouldBe("pizza, hotdog");
    }

    [Fact]
    public void ConstructorTest() {
        OrderedSet<string> set = new();
        set.Count.ShouldBe(0);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBe(0);
#endif
    }
    [Fact]
    public void ConstructorComparerTest() {
        OrderedSet<string> set = new(EqualityComparer<string>.Default);
        set.Count.ShouldBe(0);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBe(0);
#endif
    }
    [Fact]
    public void ConstructorCapacityTest() {
        OrderedSet<string> set = new(5);
        set.Count.ShouldBe(0);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBeGreaterThanOrEqualTo(5);
#endif
    }
    [Fact]
    public void ConstructorItemsTest() {
        OrderedSet<string> set = new(["pizza", "hotdog"]);
        set.Count.ShouldBe(2);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
#endif
    }
    [Fact]
    public void ConstructorItemsComparerTest() {
        OrderedSet<string> set = new(["pizza", "hotdog"], EqualityComparer<string>.Default);
        set.Count.ShouldBe(2);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
#endif
    }
    [Fact]
    public void ConstructorCapacityComparerTest() {
        OrderedSet<string> set = new(5, EqualityComparer<string>.Default);
        set.Count.ShouldBe(0);
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBeGreaterThanOrEqualTo(5);
#endif
    }
    [Fact]
    public void CountTest() {
        OrderedSet<string> set = new();
        set.Add("pizza");
        set.Add("hotdog");
        set.Add("pizza");
        set.Count.ShouldBe(2);
    }
    [Fact]
    public void CapacityTest() {
        OrderedSet<string> set = new();
        set.Add("pizza");
        set.Add("hotdog");
        set.Add("pizza");
#if NET9_0_OR_GREATER
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
#endif
    }
    [Fact]
    public void AddTest() {
        OrderedSet<string> set = new();
        set.Add("pizza");
        set.Add("hotdog");
        set.Add("pizza");
        set.ToList().ShouldBe(["pizza", "hotdog"]);
    }
    [Fact]
    public void RemoveTest() {
        OrderedSet<string> set = new(["ice cream", "pizza", "hotdog", "pizza"]);
        set.Remove("pizza");
        set.ToList().ShouldBe(["ice cream", "hotdog"]);
    }
    [Fact]
    public void ClearTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        set.Clear();
        set.Count.ShouldBe(0);
        set.ToList().ShouldBe([]);
    }
    [Fact]
    public void GetEnumeratorTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        IEnumerator<string> enumerator = set.GetEnumerator();
        List<string> list = [];
        while (enumerator.MoveNext()) {
            list.Add(enumerator.Current);
        }
        list.ShouldBe(["pizza", "hotdog"]);
    }
    [Fact]
    public void ContainsTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        set.Contains("pizza").ShouldBeTrue();
        set.Contains("hotdog").ShouldBeTrue();
        set.Contains("ice cream").ShouldBeFalse();
    }
    [Fact]
    public void CopyToArrayIndexTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        string[] array = new string[5];
        set.CopyTo(array, 1);
        array.ShouldBe([null!, "pizza", "hotdog", null!, null!]);
    }
#if NETSTANDARD2_1_OR_GREATER || NET
    [Fact]
    public void CopyToSpanTest() {
        OrderedSet<int> set = new([1, 2, 1]);
        Span<int> span = stackalloc int[5];
        set.CopyTo(span);
        span.ToArray().ShouldBe([1, 2, 0, 0, 0]);
    }
#endif
#if NET9_0_OR_GREATER
    [Fact]
    public void EnsureCapacityTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
        set.EnsureCapacity(103);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(103);
    }
    [Fact]
    public void TrimExcessTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
        set.EnsureCapacity(103);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(103);
        set.TrimExcess();
        set.Capacity.ShouldBeLessThan(103);
    }
    [Fact]
    public void TrimExcessCapacityTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(2);
        set.EnsureCapacity(103);
        set.Capacity.ShouldBeGreaterThanOrEqualTo(103);
        set.TrimExcess(30);
        set.Capacity.ShouldBeLessThan(103);
        set.TrimExcess(2);
        set.Capacity.ShouldBeLessThan(30);
    }
#endif
    [Fact]
    public void ICollectionT_IsReadOnlyTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        ((ICollection<string>)set).IsReadOnly.ShouldBeFalse();
    }
    [Fact]
    public void ICollection_IsSynchronizedTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        ((ICollection)set).IsSynchronized.ShouldBeFalse();
    }
    [Fact]
    public void ICollection_SyncRootTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        ((ICollection)set).SyncRoot.ShouldBe(set);
    }
    [Fact]
    public void ICollectionT_AddTest() {
        OrderedSet<string> set = new();
        ((ICollection<string>)set).Add("pizza");
        ((ICollection<string>)set).Add("hotdog");
        ((ICollection<string>)set).Add("pizza");
        set.ToList().ShouldBe(["pizza", "hotdog"]);
    }
    [Fact]
    public void ICollection_CopyToTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        string[] array = new string[5];
        ((ICollection)set).CopyTo(array, 1);
        array.ShouldBe([null!, "pizza", "hotdog", null!, null!]);
    }
    [Fact]
    public void ICollection_GetEnumeratorTest() {
        OrderedSet<string> set = new(["pizza", "hotdog", "pizza"]);
        IEnumerator enumerator = ((ICollection)set).GetEnumerator();
        List<object> list = [];
        while (enumerator.MoveNext()) {
            list.Add(enumerator.Current);
        }
        list.ShouldBe(["pizza", "hotdog"]);
    }
}