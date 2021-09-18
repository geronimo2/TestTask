using Volue.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Volue.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
