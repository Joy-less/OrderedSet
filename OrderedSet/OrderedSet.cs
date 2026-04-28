using System.Collections;

namespace ExtendedCollections;

/// <summary>
/// Represents an ordered set of values.
/// </summary>
public class OrderedSet<T> : ICollection<T>, ICollection, IReadOnlyCollection<T> where T : notnull {
    private readonly Dictionary<T, LinkedListNode<T>> Dictionary;
    private readonly LinkedList<T> LinkedList;

    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that is empty and uses the default equality comparer for the set type.
    /// </summary>
    public OrderedSet() {
        Dictionary = new Dictionary<T, LinkedListNode<T>>();
        LinkedList = new LinkedList<T>();
    }
    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that is empty, has the default initial capacity, and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    public OrderedSet(IEqualityComparer<T> comparer) {
        Dictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
        LinkedList = new LinkedList<T>();
    }
    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that is empty, has the specified initial capacity, and uses the default equality comparer for the set type.
    /// </summary>
    public OrderedSet(int capacity) {
        Dictionary = new Dictionary<T, LinkedListNode<T>>(capacity);
        LinkedList = new LinkedList<T>();
    }
    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that contains items copied from the specified <see cref="IEnumerable{T}"/>, has the default initial capacity, and uses the default equality comparer for the set type.
    /// </summary>
    public OrderedSet(IEnumerable<T> items)
        : this() {
        foreach (T item in items) {
            Add(item);
        }
    }
    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that contains items copied from the specified <see cref="IEnumerable{T}"/>, has the default initial capacity, and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    public OrderedSet(IEnumerable<T> items, IEqualityComparer<T> comparer)
        : this(comparer) {
        foreach (T item in items) {
            Add(item);
        }
    }
    /// <summary>
    /// Constructs a new <see cref="OrderedSet{T}"/> that contains items copied from the specified <see cref="IEnumerable{T}"/>, has the specified initial capacity, and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    public OrderedSet(int capacity, IEqualityComparer<T> comparer) {
        Dictionary = new Dictionary<T, LinkedListNode<T>>(capacity, comparer);
        LinkedList = new LinkedList<T>();
    }

    /// <summary>
    /// Gets the number of items contained in the set.
    /// </summary>
    public int Count => Dictionary.Count;
#if NET9_0_OR_GREATER
    /// <summary>
    /// Gets the total numbers of items the internal data structure can hold without resizing.
    /// </summary>
    public int Capacity => Dictionary.Capacity;
#endif

    /// <summary>
    /// Adds an item to the set if not already in the set.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the item was added to the set, otherwise, <see langword="false"/>.
    /// </returns>
    public bool Add(T item) {
        if (Dictionary.ContainsKey(item)) {
            return false;
        }
        LinkedListNode<T> node = LinkedList.AddLast(item);
        Dictionary.Add(item, node);
        return true;
    }
    /// <summary>
    /// Removes an item from the set if in the set.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the item was removed from the set, otherwise, <see langword="false"/>.
    /// </returns>
    public bool Remove(T item) {
        if (!Dictionary.TryGetValue(item, out LinkedListNode<T>? node)) {
            return false;
        }
        Dictionary.Remove(item);
        LinkedList.Remove(node);
        return true;
    }
    /// <summary>
    /// Removes every item from the set.
    /// </summary>
    public void Clear() {
        LinkedList.Clear();
        Dictionary.Clear();
    }
    /// <summary>
    /// Returns an enumerator that iterates through the set.
    /// </summary>
    public IEnumerator<T> GetEnumerator() {
        return LinkedList.GetEnumerator();
    }
    /// <summary>
    /// Determines whether the set contains the specified item.
    /// </summary>
    public bool Contains(T item) {
        return Dictionary.ContainsKey(item);
    }
    /// <summary>
    /// Copies the entire set to an array, starting at the specified index of the target array.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex) {
        LinkedList.CopyTo(array, arrayIndex);
    }
#if NETSTANDARD2_1_OR_GREATER || NET
    /// <summary>
    /// Copies the entire set to a span.
    /// </summary>
    public void CopyTo(scoped Span<T> span) {
#if NET
        ArgumentOutOfRangeException.ThrowIfLessThan(span.Length, Count);
#else
        if (span.Length < Count) {
            throw new ArgumentOutOfRangeException();
        }
#endif

        int index = 0;

        LinkedListNode<T>? node = LinkedList.First;
        while (node is not null) {
            span[index++] = node.Value;
            node = node.Next;
        }
    }
    /// <summary>
    /// Ensures that the set can hold up to a specified number of items without any further expansion of its backing storage.
    /// </summary>
    /// <returns>
    /// The new capacity of the set.
    /// </returns>
    public int EnsureCapacity(int capacity) {
        return Dictionary.EnsureCapacity(capacity);
    }
    /// <summary>
    /// Sets the capacity of the set to what it would be if it had been originally initialized with all its items.
    /// </summary>
    public void TrimExcess() {
        Dictionary.TrimExcess();
    }
    /// <summary>
    /// Sets the capacity of the set to hold up to a specified number of items without any further expansion of its backing storage.
    /// </summary>
    public void TrimExcess(int capacity) {
        Dictionary.TrimExcess(capacity);
    }
#endif

    /// <inheritdoc/>
    bool ICollection<T>.IsReadOnly => false;
    /// <inheritdoc/>
    bool ICollection.IsSynchronized => false;
    /// <inheritdoc/>
    object ICollection.SyncRoot => this;

    /// <inheritdoc/>
    void ICollection<T>.Add(T item) {
        Add(item);
    }
    /// <inheritdoc/>
    void ICollection.CopyTo(Array array, int arrayIndex) {
        if (array is not T[] typedArray) {
            throw new ArgumentException("Array is not of correct type");
        }
        CopyTo(typedArray, arrayIndex);
    }
    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}