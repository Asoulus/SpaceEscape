
using UnityEngine;

public interface ICommand
{
    string CommandWord { get; }
    string Discription { get; }
    bool Process(string[] args);
    void WrongAmmountOfArguments();
}
