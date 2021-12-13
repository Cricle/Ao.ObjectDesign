namespace System.Collections.ObjectModel
{
    public static class ObservableCollectionMoveExtensions
    {
        public static int? MoveTop<T>(this ISilentObservableCollection<T> collection, T obj)
        {
            return Move(collection, obj, CollectionMoveDirections.Top);
        }
        public static int? MoveUp<T>(this ISilentObservableCollection<T> collection, T obj)
        {
            return Move(collection, obj, CollectionMoveDirections.Up);
        }
        public static int? MoveDown<T>(this ISilentObservableCollection<T> collection, T obj)
        {
            return Move(collection, obj, CollectionMoveDirections.Down);
        }
        public static int? MoveBottom<T>(this ISilentObservableCollection<T> collection, T obj)
        {
            return Move(collection, obj, CollectionMoveDirections.Bottom);
        }
        public static int? Move<T>(this ISilentObservableCollection<T> collection, T obj, CollectionMoveDirections directions)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var index = collection.IndexOf(obj);
            if (index == -1)
            {
                return null;
            }
            switch (directions)
            {
                case CollectionMoveDirections.Top:
                    if (index != 0)
                    {
                        collection.Move(index, 0);
                        return 0;
                    }
                    break;
                case CollectionMoveDirections.Up:
                    if (index != 0)
                    {
                        collection.Move(index, index - 1);
                        return index - 1;
                    }
                    break;
                case CollectionMoveDirections.Down:
                    if (index != collection.Count - 1)
                    {
                        collection.Move(index, index + 1);
                        return index + 1;
                    }
                    break;
                case CollectionMoveDirections.Bottom:
                    var count = collection.Count;
                    if (index != count - 1)
                    {
                        collection.Move(index, count - 1);
                        return count - 1;
                    }
                    break;
                default:
                    throw new NotSupportedException(directions.ToString());
            }
            return null;
        }
    }
}
