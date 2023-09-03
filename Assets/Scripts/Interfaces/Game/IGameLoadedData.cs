using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGameLoadedData
{
    Type Type { get; }
    bool IsLoaded { get; }
    void SetData(object data);
    object PullOutData();
}
