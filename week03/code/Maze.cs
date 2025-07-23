/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;
    private ValueTuple<int, int> _currentPosition; // Track the current position


    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap ?? throw new ArgumentNullException(nameof(mazeMap));;

        
        _currentPosition = (_currX, _currY); // Starting position as per test
    }

    // TODO Problem 4 - ADD YOUR CODE HERE
    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
          if (!_mazeMap.TryGetValue(_currentPosition, out var movements) || !movements[0]) // movements[0] is 'left'
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentPosition = (_currentPosition.Item1 - 1, _currentPosition.Item2);
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
         if (!_mazeMap.TryGetValue(_currentPosition, out var movements) || !movements[1]) // movements[1] is 'right'
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentPosition = (_currentPosition.Item1 + 1, _currentPosition.Item2);
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        if (!_mazeMap.TryGetValue(_currentPosition, out var movements) || !movements[2]) // movements[2] is 'up'
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentPosition = (_currentPosition.Item1, _currentPosition.Item2 + 1);
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
      if (!_mazeMap.TryGetValue(_currentPosition, out var movements) || !movements[3]) // movements[3] is 'down'
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentPosition = (_currentPosition.Item1, _currentPosition.Item2 - 1);
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}