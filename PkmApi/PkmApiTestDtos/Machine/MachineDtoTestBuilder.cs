using PkmApi.Dtos.Machine.Machine;

namespace PkmApiTestDtos.Machine
{
    public class MachineDtoTestBuilder : IDtoTestBuilder<MachineDto>
    {
        #region IDtoTestBuilder<MachineDto>
        public MachineDto GetBasic()
        {
            return new(1);
        }

        public MachineDto GetEmpty()
        {
            return new(1, new("item", "items/1"), new("move", "moves/1"), new("version-group", "version-groups/1"));
        }

        public MachineDto GetFull()
        {
            return new(
                1, 
                new("item", "items/1"), 
                new("move", "moves/1"), 
                new("version-group", "version-groups/1")
            );
        }

        public MachineDto GetShallow()
        {
            return new(
                1,
                new("item", "items/1"),
                new("move", "moves/1"),
                new("version-group", "version-groups/1")
            );
        }
        #endregion
    }
}
