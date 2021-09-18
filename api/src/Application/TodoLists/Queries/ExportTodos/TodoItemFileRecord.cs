using Volue.Application.Common.Mappings;
using Volue.Domain.Entities;

namespace Volue.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
