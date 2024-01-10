namespace PaperRename2.Services;

public interface ITextController
{
    void MakeItShort(object textBox);
    void TittleSimplify(object textBox);
    void MakeItTitle(object textBox);
    void MakeUpper(object textBox);
    void MakeLower(object textBox);
}