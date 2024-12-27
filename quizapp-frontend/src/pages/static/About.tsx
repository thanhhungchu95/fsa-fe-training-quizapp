import { useEffect, useState } from "react";
import IMemberInformation from "../../models/IMemberInformation";
import LandingContent from "../../shared/components/static/LandingContent";
import MemberInformation from "../../shared/components/MemberInformation";
import avatar1 from "../../assets/pictures/avatar-1.png";
import avatar2 from "../../assets/pictures/avatar-6.png";
import avatar3 from "../../assets/pictures/avatar-9.png";

const About = () => {
    const [data, setData] = useState<IMemberInformation[]>([]);

    useEffect(() => {
        setData([{
            key: 1,
            name: 'John Doe',
            position: 'Front End Developer',
            thumbnailUrl: avatar1,
        },
        {
            key: 2,
            name: 'Jane Doe',
            position: 'Back End Developer',
            thumbnailUrl: avatar2,
        },
        {
            key: 3,
            name: 'John Smith',
            position: 'Fullstack Developer',
            thumbnailUrl: avatar3,
        }]);
    }, []);

    return (
        <div className="h-dvh px-64">
            <LandingContent heading={"About us"} />
            <section className="w-full h-1/3 px-3.5 flex flex-col items-center">
                <p className="uppercase font-bold text-4xl pb-4">Our Team</p>
                <div className="grid grid-cols-3 gap-4 space-x-3 justify-items-stretch min-h-0">
                    {
                        data.map((member: IMemberInformation) => (
                            <MemberInformation key={member.key} name={member.name} position={member.position} thumbnailUrl={member.thumbnailUrl} />
                        ))
                    }
                </div>
            </section>
        </div>
    );
}

export default About;