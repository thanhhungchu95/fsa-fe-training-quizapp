import IMemberInformation from "../../models/IMemberInformation";

const MemberInformation = ({ key, name, position, thumbnailUrl }: IMemberInformation) => {
    return (
        <div key={key} className="p-2 border border-blue-700 rounded-lg flex flex-col space-y-3 min-h-0 items-center">
            <img src={thumbnailUrl} alt={name} className="h-1/2 w-full rounded-sm" />
            <p className="text-lg font-bold">{name}</p>
            <p className="text-sm">{position}</p>
        </div>
    );
}

export default MemberInformation;