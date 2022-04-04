using System.Collections.Generic;

namespace ToDoCqrs.Domains.Entities
{
    public class UserEntity : Entity
    {
        public string Email { get; set; }

        public List<ToDoEntity> ToDoList { get; set; }
        
        public UserEntity(string email)
            : base()
        {
            this.Email = email;
        }

        public void AddToDo(ToDoEntity toDo)
        {
            if(this.ToDoList is null)
                this.ToDoList = new List<ToDoEntity>();
                
            this.ToDoList.Add(toDo);
        }

        public ToDoEntity Done(string id)
        {
            int index = ToDoList.FindIndex(x => x.Id == id);
            if(index < 0)
                return null;
        
            ToDoEntity toDo = ToDoList[index];
            toDo.Done();
            ToDoList[index] = toDo;
            return toDo;
            
        }

    }
}